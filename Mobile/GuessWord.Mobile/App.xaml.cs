using GuessWord.Mobile.Services;
using GuessWord.Mobile.ViewModels;
using GuessWord.Mobile.Views;
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
            ServiceProvider = services.BuildServiceProvider();

            MainPage = new SignInView();
            // MainPage = new NavigationPage(new SignIn());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
