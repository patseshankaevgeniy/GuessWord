using GuessWord.Mobile.Application.Common;
using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.Common.Models;
using GuessWord.Mobile.Application.Common.Services;
using GuessWord.Mobile.Application.UserWords.Controllers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.Application.UserWords.ViewModels
{
    public class SearchWordViewModel : BaseViewModel
    {
        private readonly IWordService _wordService;
        private readonly INavigationService _navigationService;
        private readonly AddUserWordController _wordController;

        public Word SelectedWord { get; set; }
        public ObservableCollection<Word> SearchWords { get; set; }

        public Command AddWordCommand { get; set; }
        public Command GoBackCommand { get; set; }

        public SearchWordViewModel(IWordService wordService,
                                   INavigationService navigationService,
                                   AddUserWordController wordController)
        {
            _wordService = wordService;
            _navigationService = navigationService;
            _wordController = wordController;
            SearchWords = new ObservableCollection<Word>();
            AddWordCommand = new Command(async () => await _navigationService.NavigateToAddWordViewAfterSearchAsync(SelectedWord.Value));
            GoBackCommand = new Command(async () => await _navigationService.NavigateBackFromSearchAsync());
        }

        public async Task SearchWordsAsync(string letter)
        {
            var words = await _wordService.GetByLetterAsync(letter);

            if (words == null)
            {
                SearchWords.Clear();
                SearchWords.Add(new Word { Value = letter });
            }
            else
            {
                SearchWords.Clear();
                SearchWords.Add(new Word { Value = letter });
                foreach (var word in words)
                {
                    SearchWords.Add(word);
                }
            }
        }




    }
}
