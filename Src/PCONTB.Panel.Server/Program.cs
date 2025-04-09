using PCONTB.Panel.Application;
using PCONTB.Panel.Infrastructure;
using PCONTB.Panel.Infrastructure.Security;
using PCONTB.Panel.Infrastructure.Security.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var configuration = builder.Configuration;

builder.Services.AddInfrastructure(configuration);
builder.Services.AddInfrastructureSecurity(configuration);
builder.Services.AddApplication();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseMiddleware<SessionMiddleware>();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();