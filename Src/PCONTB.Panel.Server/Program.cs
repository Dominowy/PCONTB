using PCONTB.Panel.Application;
using PCONTB.Panel.Application.Common.EnumHelpers;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Infrastructure;
using PCONTB.Panel.Server.Middleware;
using PCONTB.Panel.Server.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new LowercaseEnumConverter());
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var configuration = builder.Configuration;

builder.Services.AddLogging();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication(configuration);

builder.Services.AddScoped<ICookieService, CookieService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
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