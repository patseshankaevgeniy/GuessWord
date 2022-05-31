using GuessWord.Mobile.Application.Auth.ViewModels;
using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.Common.Models;
using GuessWord.Mobile.Application.UserWords.ViewModels;
using GuessWord.Mobile.Views;
using GuessWord.Mobile.Views.Auth;
using GuessWord.Mobile.Views.Game;
using GuessWord.Mobile.Views.Settings;
using GuessWord.Mobile.Views.UserWords;
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
            Routing.RegisterRoute(nameof(AddWordView), typeof(AddWordView));
            Routing.RegisterRoute(nameof(SearchWordView), typeof(SearchWordView));
            Routing.RegisterRoute(nameof(EditWordView), typeof(EditWordView));
        }

        #region Auth
        public async Task NavigateToSignInAsync()
        {
            await _shell.GoToAsync($"//{nameof(SignInView)}");
        }
        public async Task NavigateToSignUpAsync(string login)
        {
            await _shell.GoToAsync($"//{nameof(SignInView)}/{nameof(SignUpView)}?{nameof(SignUpViewModel.Login)}={login}");
        }
        public async Task NavigateToSignInAfterRegistreAsync(string login, string password)
        {
            await _shell.GoToAsync($"//{nameof(SignInView)}?{nameof(SignInViewModel.Login)}={login}&{nameof(SignInViewModel.Password)}={password}");
        }

        #endregion

        #region UserWords
        public async Task NavigateToUserWordAsync(bool wordChaneged)
        {
            await _shell.GoToAsync($"//{nameof(UserWordView)}?{nameof(UserWordViewModel.SourceChanged)}={wordChaneged}");
        }
        public async Task NavigateToAddWordViewAsync()
        {
            await _shell.GoToAsync($"///{nameof(UserWordView)}//{nameof(AddWordView)}");
        }
        public async Task NavigateToSearchWordViewAsync()
        {
            await _shell.GoToAsync($"//{nameof(UserWordView)}/{nameof(AddWordView)}/{nameof(SearchWordView)}");
        }
        public async Task NavigateBackFromSearchAsync()
        {
            await _shell.GoToAsync($"//{nameof(SearchWordView)}/{nameof(AddWordView)}");
        }
        public async Task NavigateToAddWordViewAfterSearchAsync(string word)
        {
            await _shell.GoToAsync($"//{nameof(UserWordView)}/{nameof(AddWordView)}?{nameof(AddWordViewModel.Word)}={word}&{nameof(AddWordViewModel.WordSelected)}={true}");
        }
        public async Task NavigateToEditWordViewAsync(int wordId)
        {
            await _shell.GoToAsync($"//{nameof(UserWordView)}/{nameof(EditWordView)}?{nameof(EditWordViewModel.WordId)}={wordId}");
        }

        #endregion

        #region Game
        public async Task NavigateToPlayViewAsync()
        {
            await _shell.GoToAsync($"//{nameof(PlayView)}");
        }
        public async Task NavigateToFinishGameAsync()
        {
            await _shell.GoToAsync($"//{nameof(FinishGameView)}");
        }
        #endregion

        public async Task NavigateBackAsync()
        {
            await _shell.GoToAsync("..");
        } 
        public async Task NavigateToMainAsync()
        {
            await _shell.GoToAsync($"//{nameof(HomeView)}");
        }

    }
}
