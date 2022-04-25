using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class AddWordViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public Command GoToSearchBarViewCommand { get; set; }

        public AddWordViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoToSearchBarViewCommand = new Command(async () => await GoToSearchBar());

        }

        private async Task GoToSearchBar()
        {
            await _navigationService.NavigateToSearchWordViewAsync();
        }
    }
}
