using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteDataSystem.Application.Interfaces;
using TesteDataSystem.Application.Mappings;
using TesteDataSystem.Application.Services;
using TesteDataSystem.Domain.Interfaces;
using TesteDataSystem.Infrastructure.Context;
using TesteDataSystem.Infrastructure.Repositories;

namespace TesteDataSystem.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            services.AddScoped<IDataBaseRepository, DataBaseRepository>();

            services.AddScoped<IDataBaseService, DataBaseService>();

            return services;
        }
    }
}
