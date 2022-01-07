using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public Command GoToPlayCommand { get; set; }

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoToPlayCommand = new Command(NavigateToPlayView);
        }

        public async void NavigateToPlayView()
        {
            await _navigationService.NavigateToPlayViewAsync();
        }
    }
}
