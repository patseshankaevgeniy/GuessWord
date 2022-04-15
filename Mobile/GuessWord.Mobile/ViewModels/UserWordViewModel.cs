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

        public UserWord SelectedWord { get; set; }
        public IList<UserWord> UserWords { get; set; }

        public Command NavigateToWordCreateViewCommand { get; set; }




        public Command<UserWord> NavigateToWordDetailsCommand { get; set; }

        public UserWordViewModel(
            INavigationService navigationService,
            IUserWordService userWordService)
        {
            _navigationService = navigationService;
            _userWordService = userWordService;
            UserWords = new List<UserWord>();
            NavigateToWordDetailsCommand = new Command<UserWord>(async (userWord) => await NavigateToWordDetailsAsync());
            NavigateToWordCreateViewCommand = new Command(async () => await NavigateToCreateWordViewAsync());
        }

        private async Task NavigateToCreateWordViewAsync()
        {
            await _navigationService.NavigateToCreateWordViewAsync();
        }

        public async Task NavigateToWordDetailsAsync()
        {
            await _navigationService.NavigateToWordViewAsync(SelectedWord.Id);
        }

        public override async Task OnInitializedAsync()
        {
            var userWords = await _userWordService.GetAllAsync();

            if (userWords != null)
            {
                UserWords.Clear();

                foreach (var userWord in userWords)
                {
                    UserWords.Add(userWord);
                }
            }
        }
    }
}
