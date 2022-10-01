using GuessWord.Application.Common.Interfaces;
using GuessWord.Application.Common.Interfaces.Repositories;
using GuessWord.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GuessWord.Persistence
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
