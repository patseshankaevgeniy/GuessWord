using GuessWord.Application.Auth;
using GuessWord.Application.Levels;
using GuessWord.Application.UserWords;
using GuessWord.Application.UserWords.Models;
using GuessWord.Application.Words;
using GuessWord.Application.Words.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GuessWord.Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILevelsService, LevelsService>();
            services.AddScoped<IUserWordsService, UserWordsService>();
            services.AddScoped<IWordsService, WordsService>();

            // Mappers
            services.AddScoped<IUserWordMapper, UserWordMapper>();
            services.AddScoped<IWordMapper, WordMapper>();

            return services;
        }
    }
}
