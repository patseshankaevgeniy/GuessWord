using GuessWord.Mobile.ViewModels;
using GuessWord.Mobile.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Shell _shell;

        public NavigationService()
        {
            _shell = Shell.Current;
            Routing.RegisterRoute(nameof(PlayView), typeof(PlayView));
            Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
            Routing.RegisterRoute(nameof(SignInView), typeof(SignInView));
            Routing.RegisterRoute(nameof(SignUpView), typeof(SignUpView));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(FinishGameView), typeof(FinishGameView));
            Routing.RegisterRoute(nameof(UserWordView), typeof(UserWordView));
            Routing.RegisterRoute(nameof(WordDetailsView), typeof(WordDetailsView));
            Routing.RegisterRoute(nameof(CreateWordView), typeof(CreateWordView));
        }

        public async Task NavigateToSignInAsync()
        {
            await _shell.GoToAsync($"//{nameof(SignInView)}");
        }

        public async Task NavigateToSignUpAsync(string login)
        {
            await _shell.GoToAsync($"//{nameof(SignInView)}/{nameof(SignUpView)}?{nameof(SignUpViewModel.Login)}={login}");
        }

        public async Task NavigateBackAsync()
        {
            await _shell.GoToAsync("..");
        }

        public async Task NavigateToMainAsync()
        {
            await _shell.GoToAsync($"//{nameof(HomeView)}");
        }

        public async Task NavigateToSignInAfterRegistreAsync(string login, string password)
        {
            await _shell.GoToAsync($"//{nameof(SignInView)}?{nameof(SignInViewModel.Login)}={login}&{nameof(SignInViewModel.Password)}={password}");
        }

        public async Task NavigateToPlayViewAsync()
        {
            await _shell.GoToAsync($"//{nameof(PlayView)}");
        }

        public async Task NavigateToFinishGameAsync()
        {
            await _shell.GoToAsync($"//{nameof(FinishGameView)}");
        }

        public async Task NavigateToUserWordAsync()
        {
            await _shell.GoToAsync($"//{nameof(UserWordView)}");
        }

        public async Task NavigateToWordViewAsync(int wordId)
        {
            await _shell.GoToAsync($"//{nameof(UserWordView)}/{nameof(WordDetailsView)}?{nameof(WordDetailsViewModel.WordId)}={wordId}");
        }

        public async Task NavigateToCreateWordViewAsync()
        {
            await _shell.GoToAsync($"//{nameof(CreateWordView)}");
        }
    }
}
