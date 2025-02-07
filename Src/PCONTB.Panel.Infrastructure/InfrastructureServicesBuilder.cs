using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCONTB.Panel.Application.Contracts.DbContext;
using PCONTB.Panel.Infrastructure.Context;

namespace PCONTB.Panel.Infrastructure
{
    public static class InfrastructureServicesBuilder
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
