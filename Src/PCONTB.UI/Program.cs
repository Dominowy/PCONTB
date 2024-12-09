
using AspNetCore.Proxy;
using RimuTec.AspNetCore.SpaServices.WebpackDevelopmentServer;
using RimuTec.AspNetCore.SpaServices.Extensions;

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
                configuration.RootPath = "Data";
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
                spa.Options.SourcePath = "/App";
                spa.Options.DevServerPort = 3000;

                if (app.Environment.IsDevelopment())
                {
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(120);
                    spa.UseProxyToSpaDevelopmentServer($"http://localhost:{spa.Options.DevServerPort}");
                    //spa.UseWebpackDevelopmentServer(npmScriptName: "start");
                }
            });

            app.Run();
        }
    }
}
