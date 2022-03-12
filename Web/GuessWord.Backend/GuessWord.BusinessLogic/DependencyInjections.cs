using GuessWord.BusinessLogic.Services;
using GuessWord.BusinessLogic.Services.Interfaces;
using GuessWord.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GuessWord.BusinessLogic
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILevelsService, LevelsService>();
            services.AddScoped<IWordsRepository, WordsRepository>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUserWordsService, UserWordsService>();

            
            return services;
        }
    }
}
