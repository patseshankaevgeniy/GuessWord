using GuessWord.Mobile.Application.Common;
using GuessWord.Mobile.Application.Common.Interfaces;
using Xamarin.Forms;

namespace GuessWord.Mobile.Application.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public Command GoToPlayCommand { get; set; }
        public Command GoToUserWordCommand { get; set; }
        public Command GoToCreateWordCommand { get; set; }

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoToPlayCommand = new Command(NavigateToPlayView);
            GoToUserWordCommand = new Command(NavigateToUserWordView);
        }


        private async void NavigateToUserWordView()
        {
            await _navigationService.NavigateToUserWordAsync();
        }

        public async void NavigateToPlayView()
        {
            await _navigationService.NavigateToPlayViewAsync();
        }
    }
}
