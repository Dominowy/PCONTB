using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCONTB.Panel.Domain.Repositories;
using PCONTB.Panel.Infrastructure.Context;
using PCONTB.Panel.Infrastructure.Repositories;

namespace PCONTB.Panel.Infrastructure
{
    public static class InfrastructureServicesBuilder
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
