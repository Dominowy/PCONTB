using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PCONTB.Panel.Application.Contracts.Application.Services.Auth;
using PCONTB.Panel.Application.Services.Auth;
using System.Reflection;

namespace PCONTB.Panel.Application
{
    public static class ApplicationServiceBuilder
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<ISessionService, SessionService>();

            return services;
        }
    }
}
