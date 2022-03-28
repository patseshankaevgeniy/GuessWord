using GuessWord.Mobile.Clients;
using GuessWord.Mobile.Services;
using GuessWord.Mobile.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace GuessWord.Mobile
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider;

        public App()
        {
            InitializeComponent();
            
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
                System.Exception ex = (System.Exception)args.ExceptionObject;
                Console.WriteLine(ex);
            };

            var services = new ServiceCollection();
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<SignInViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<PlayViewModel>();
            services.AddTransient<FinishGameViewModel>();
            services.AddTransient<UserWordViewModel>();
            services.AddTransient<WordDetailsViewModel>();
            services.AddSingleton<ILevelsController, LevelsController>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IBackendClient, BackendClient>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            //services.AddSingleton<ICurrentUserService, FakeCurrentUserService>();
            services.AddSingleton<IUserWordService, UserWordService>();
            //services.AddSingleton<IUserWordService, FakeUserWordService>();
            services.AddTransient<IGuessWordApiClient>(services=>
            {
                try
                {
                var currentUserService = services.GetService<ICurrentUserService>();
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentUserService.AccessToken);
                return new GuessWordApiClient("https://4712-178-172-234-92.ngrok.io/", httpClient);
                }
                catch (Exception)
                {

                    throw;
                }
            });
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
