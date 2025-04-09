using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCONTB.Panel.Application.Contracts.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth;
using PCONTB.Panel.Application.Contracts.Infrastructure.Security.Auth.Encryption;
using PCONTB.Panel.Infrastructure.Security.Auth;
using PCONTB.Panel.Infrastructure.Security.Auth.Encryption;

namespace PCONTB.Panel.Infrastructure.Security
{
    public static class InfrastructureSecurityServicesBuilder
    {
        public static IServiceCollection AddInfrastructureSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenSettings = new TokenSettings();
            configuration.GetSection("TokenSettings").Bind(tokenSettings);

            services.AddSingleton(tokenSettings);

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<ICookieService, CookieService>();

            return services;
        }
    }
}
