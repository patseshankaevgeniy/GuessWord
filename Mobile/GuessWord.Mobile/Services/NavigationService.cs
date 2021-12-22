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
            Routing.RegisterRoute(nameof(WordsView), typeof(WordsView));
            Routing.RegisterRoute(nameof(SignInView), typeof(SignInView));
            Routing.RegisterRoute(nameof(SignUpView), typeof(SignUpView));
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
            await _shell.GoToAsync($"//{nameof(WordsView)}");
        }
    }
}
