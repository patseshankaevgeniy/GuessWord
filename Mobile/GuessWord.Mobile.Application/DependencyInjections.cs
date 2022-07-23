using GuessWord.Mobile.Application.Auth.Services;
using GuessWord.Mobile.Application.Auth.ViewModels;
using GuessWord.Mobile.Application.Common.Services;
using GuessWord.Mobile.Application.Game.Controllers;
using GuessWord.Mobile.Application.Game.ViewModels;
using GuessWord.Mobile.Application.Home;
using GuessWord.Mobile.Application.UserWords;
using GuessWord.Mobile.Application.UserWords.Services;
using GuessWord.Mobile.Application.UserWords.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GuessWord.Mobile.Application
{
    public static class DependencyInjections
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            // ViewModels
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<SignInViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<PlayViewModel>();
            services.AddTransient<FinishGameViewModel>();
            services.AddTransient<UserWordViewModel>();
            services.AddTransient<AddWordViewModel>();
            services.AddTransient<SearchWordViewModel>();
            services.AddTransient<EditWordViewModel>();

            // Controllers
            services.AddSingleton<ILevelsController, LevelsController>();
            services.AddSingleton<UserWordsController>();

            // Services

            services.AddSingleton<IAuthService, AuthService>();
            //services.AddSingleton<IUserWordService, UserWordService>();
            services.AddSingleton<FakeUserWordService>();
            services.AddSingleton<IUserWordService>(x => x.GetService<FakeUserWordService>());
            services.AddSingleton<IUserWordEditService>(x => x.GetService<FakeUserWordService>());
            //services.AddSingleton<IWordService, WordService>();
            services.AddSingleton<IWordService, FakeWordService>();

        }
    }
}
