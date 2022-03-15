using GuessWord.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GuessWord.DataAccess
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
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
