using Microsoft.Extensions.Hosting.Internal;
using RimuTec.AspNetCore.SpaServices.WebpackDevelopmentServer;

namespace PCONTB.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSpaStaticFiles(configuration =>
            {
                var dir = GetAppDir(builder);
                // This is where files will be served from in non-Development environments
                configuration.RootPath = Path.Combine(dir, "dist"); // In Development environments, the content of this folder will be deleted
                Directory.CreateDirectory(configuration.RootPath);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = GetAppDir(builder);
                spa.Options.DevServerPort = 3000;

                if (app.Environment.IsDevelopment())
                {
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(120);
                    spa.UseProxyToSpaDevelopmentServer($"http://localhost:{spa.Options.DevServerPort}");
                    spa.UseWebpackDevelopmentServer(npmScriptName: "start");
                }
            });

            app.Run();
        }

        private string GetAppDir(WebApplicationBuilder builder)
        {
            // TopShelf cos psuje ze œcie¿kami - taki brzydki hack ¿eby w visualu ³adowa³o siê dobrze.
            var appDir = Path.Combine(builder.Environment.ContentRootPath, "App");
            if (!Directory.Exists(Path.Combine(appDir, "src")) && !builder.Environment.IsProduction())
            {
                var path = Path.Combine(builder.Environment.ContentRootPath, "..\\..\\..\\App");
                var path2 = Path.Combine(builder.Environment.ContentRootPath, "..\\..\\..\\..\\App");

                if (Directory.Exists(Path.Combine(path)))
                {
                    appDir = path;
                }
                else if (Directory.Exists(path2))
                {
                    appDir = path2;
                }
                else
                {
                    appDir = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName((builder.Environment.ContentRootPath)))), "App");
                }
            }

            return Path.GetFullPath(appDir);
        }
    }
}
