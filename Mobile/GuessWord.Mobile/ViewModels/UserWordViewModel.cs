using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Models;
using GuessWord.Mobile.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class UserWordViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWordService _userWordService;

        public bool IsNoWordsVisible { get; set; }
        public bool IsWordsVisible { get; set; }

        public UserWord SelectedWord { get; set; }
        public IList<UserWord> UserWords { get; set; }

        public Command NavigateToWordCreateViewCommand { get; set; }
        public Command<UserWord> NavigateToEditWordViewCommand { get; set; }

        public UserWordViewModel(
            INavigationService navigationService,
            IUserWordService userWordService)
        {
            _navigationService = navigationService;
            _userWordService = userWordService;
            UserWords = new List<UserWord>();
            NavigateToEditWordViewCommand = new Command<UserWord>(async (userWord) => await NavigateToEditWordViewAsync());
            NavigateToWordCreateViewCommand = new Command(async () => await NavigateToCreateWordViewAsync());
            IsWordsVisible = true;
            IsNoWordsVisible = false;

        }

        public override async Task OnInitializedAsync()
        {
            var userWords = await _userWordService.GetAllAsync();

            if (userWords != null)
            {
                IsNoWordsVisible = false;
                UserWords.Clear();

                foreach (var userWord in userWords)
                {
                    UserWords.Add(userWord);
                }
            }
            if (userWords == null)
            {
                IsNoWordsVisible = true;
                IsWordsVisible = false;
            }
        }

        private async Task NavigateToCreateWordViewAsync()
        {
           await _navigationService.NavigateToAddWordViewAsync();
        }

        public async Task NavigateToEditWordViewAsync()
        {
            await _navigationService.NavigateToEditWordViewAsync(SelectedWord.Id);
        }
    }
}
