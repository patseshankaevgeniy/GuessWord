using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class SearchWordViewModel : BaseViewModel
    {
        private readonly IWordService _wordService;
        private readonly INavigationService _navigationService;

        public Models.Word SelectedWord { get; set; }
        public ObservableCollection<Models.Word> SearchWords { get; set; }

        public Command AddWordCommand { get; set; }

        public SearchWordViewModel(IWordService wordService,
                                   INavigationService navigationService)
        {
            _wordService = wordService;
            _navigationService = navigationService;
            SearchWords = new ObservableCollection<Models.Word>();
            AddWordCommand = new Command(async (SelectedWord) => await GoToAddWordViewAsync());
        }

        private async Task GoToAddWordViewAsync()
        {
            await _navigationService.NavigateBackAsync();
        }

        public async Task SearchWordsAsync(string letter)
        {
            var words = await _wordService.GetByLetterAsync(letter);
            foreach (var word in words)
            {
                SearchWords.Add(word);
            }
        }




    }
}
