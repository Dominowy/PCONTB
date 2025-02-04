
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PCONTB.Panel.Application
{
    public static class ApplicationServiceBuilder
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
