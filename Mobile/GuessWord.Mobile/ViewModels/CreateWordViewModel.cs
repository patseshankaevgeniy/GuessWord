using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Models;
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
        public bool IsWordErrorVisible { get; set; }

        public string Translation { get; set; }
        public string TranslationErrorText { get; set; }
        public bool IsTranslationErrorVisible { get; set; }

        public Command CreateCommand { get; set; }

        public CreateWordViewModel(
                IUserWordService userWordService,
                INavigationService navigationService)
        {
            _userWordService = userWordService;
            _navigationService = navigationService;
            CreateCommand = new Command(async ()=> await CreateAsync());
        }

        private async Task CreateAsync()
        {
            if (!Validate()) { return; }

            var result = await _userWordService.CreateAsync(Word);

        }

        public bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrEmpty(Word))
            {
                WordErrorText = "Name is empty.";
                IsWordErrorVisible = true;
                isValid = false;
            }
            else
            {
                IsWordErrorVisible = false;
            }

            if (string.IsNullOrEmpty(Translation))
            {
                TranslationErrorText = "Translation is empty.";
                IsTranslationErrorVisible = true;
                isValid = false;
            }
            else
            {
                IsTranslationErrorVisible = false;
            }
            return isValid;
        }
    }
}
