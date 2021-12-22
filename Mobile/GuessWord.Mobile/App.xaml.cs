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
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<INavigationService, NavigationService>();
            ServiceProvider = services.BuildServiceProvider();

            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            var authService = ServiceProvider.GetService<IAuthService>();
            if (!await authService.CheckSignInAsync())
            {
                var navigationService = ServiceProvider.GetService<INavigationService>();
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
