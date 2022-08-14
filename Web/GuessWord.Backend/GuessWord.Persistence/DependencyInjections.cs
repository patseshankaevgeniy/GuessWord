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
            services.AddScoped<IUserWordsRepository, UserWordsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWordsRepository, WordsRepository>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
