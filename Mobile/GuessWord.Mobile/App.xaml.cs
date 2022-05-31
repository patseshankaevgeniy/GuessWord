using GuessWord.Mobile.Application;
using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.Common.Services;
using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GuessWord.Mobile
{
    public partial class App : Xamarin.Forms.Application
    {
        public static IServiceProvider ServiceProvider;

        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();

            services.AddApplicationDependencies();
            services.AddInfrastructureDependencies();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IPropertiesStorage, PropertiesStorage>();
            services.AddSingleton<IPopupService, PopupService>();

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
