using GuessWord.Mobile.Services;
using GuessWord.Mobile.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace GuessWord.Mobile
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider;

        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<SignInViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<PlayViewModel>();
            services.AddTransient<FinishGameViewModel>();
            services.AddSingleton<ILevelsController, LevelsController>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IBackendClient, BackendClient>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            ServiceProvider = services.BuildServiceProvider();

            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            var currentUserService = ServiceProvider.GetService<ICurrentUserService>();
            var navigationService = ServiceProvider.GetService<INavigationService>();

            if (currentUserService.IsSignedIn)
            {
                await navigationService.NavigateToMainAsync();
            }
            else
            {
                await navigationService.NavigateToSignInAsync();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
