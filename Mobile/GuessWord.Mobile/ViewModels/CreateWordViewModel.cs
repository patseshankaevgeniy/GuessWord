using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class CreateWordViewModel : BaseViewModel
    {
        private readonly IUserWordService _userWordService;
        private readonly INavigationService _navigationService;

        public string Word { get; set; }
        public string WordErrorText { get; set; }
        public bool IsWordVisible { get; set; }

        public string Translation { get; set; }
        public string TranslationErrorText { get; set; }
        public bool IsSearchBarVisible { get; set; }

        public Command OpenSearchBarCommand { get; set; }
        public Command CreateWordCommand { get; set; }

        public CreateWordViewModel(
                IUserWordService userWordService,
                INavigationService navigationService)
        {
            _userWordService = userWordService;
            _navigationService = navigationService;
            CreateWordCommand = new Command(async () => await CreateAsync());
            OpenSearchBarCommand = new Command(OpenSearchBar);
            IsWordVisible = true;
            IsSearchBarVisible = false;
        }

        private void OpenSearchBar()
        {
            IsWordVisible = false;
            IsSearchBarVisible = true;
        }

        private async Task CreateAsync()
        {
            if (!Validate()) { return; }

            var result = await _userWordService.CreateAsync(Word);

        }

        public bool Validate()
        {
            var isValid = true;
            return isValid;
        }
    }
}
