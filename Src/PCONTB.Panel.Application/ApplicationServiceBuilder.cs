using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth.Encryption;
using PCONTB.Panel.Application.Services.Auth;
using PCONTB.Panel.Infrastructure.Security.Auth;
using PCONTB.Panel.Infrastructure.Security.Auth.Encryption;
using System.Reflection;

namespace PCONTB.Panel.Application
{
    public static class ApplicationServiceBuilder
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            var tokenSettings = new TokenSettings();
            configuration.GetSection("TokenSettings").Bind(tokenSettings);

            services.AddSingleton(tokenSettings);

            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionAccesor, SessionAccesor>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
