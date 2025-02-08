using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PCONTB.Panel.Application.Functions.Projects.Projects.Commands;
using System.Reflection;

namespace PCONTB.Panel.Application
{
    public static class ApplicationServiceBuilder
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
