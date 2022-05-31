using GuessWord.Mobile.Application.Common;
using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.UserWords.Models;
using GuessWord.Mobile.Application.UserWords.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GuessWord.Mobile.Application.UserWords.ViewModels
{
    [QueryProperty(nameof(SourceChanged), nameof(SourceChanged))]
    public class UserWordViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWordService _userWordService;

        public bool IsNoWordsVisible { get; set; }
        public bool IsWordsVisible { get; set; }
        public bool SourceChanged { get; set; }

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
            NavigateToWordCreateViewCommand = new Command(async () => await NavigateToAddWordViewAsync());
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
            else
            {
                IsNoWordsVisible = true;
                IsWordsVisible = false;
            }
        }

        public override async Task OnAppearingAsync()
        {
            if (SourceChanged)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    SourceChanged = false;
                    await OnInitializedAsync();
                });
            }
        }

        private async Task NavigateToAddWordViewAsync()
        {
            await _navigationService.NavigateToAddWordViewAsync();
        }

        public async Task NavigateToEditWordViewAsync()
        {
            await _navigationService.NavigateToEditWordViewAsync(SelectedWord.Id);
        }
    }
}
