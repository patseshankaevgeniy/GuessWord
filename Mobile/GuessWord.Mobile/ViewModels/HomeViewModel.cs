using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;
using System;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public Command GoToPlayCommand { get; set; }
        public Command GoToUserWordCommand { get; set; }

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
