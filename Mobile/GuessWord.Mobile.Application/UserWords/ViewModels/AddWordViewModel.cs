using GuessWord.Mobile.Application.Common;
using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.Common.Models;
using GuessWord.Mobile.Application.Common.Services;
using GuessWord.Mobile.Application.UserWords.Controllers;
using GuessWord.Mobile.Application.UserWords.Models;
using GuessWord.Mobile.Application.UserWords.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.Application.UserWords.ViewModels
{
    [QueryProperty(nameof(Word), nameof(Word))]
    [QueryProperty(nameof(WordSelected), nameof(WordSelected))]
    public class AddWordViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWordService _userWordService;
        private readonly IWordService _wordService;
        private readonly IPopupService _popupService;

        public Word UserWord { get; set; }
        public bool WordSelected { get; set; }
        public bool AddWordButtonVisible { get; set; }

        public string Word { get; set; }
        public bool AddWordVisible { get; set; }
        public Command GoToSearchWordCommand { get; set; }
        public Command AddNewUserWordCommand { get; set; }

        public ObservableCollection<Translation> Translations { get; set; }
        public bool IsTranslationsVisible { get; set; }
        public Command AddTranslationCommand { get; set; }
        public Command SaveNewTranslationCommand { get; set; }
        public Command DeleteTranslationCommand { get; set; }

        public string Status { get; set; }
        public bool StatusVisible { get; set; }

        public AddWordViewModel(INavigationService navigationService,
                                IUserWordService userWordService,
                                IWordService wordService,
                                AddUserWordController wordController,
                                IPopupService popupService)
        {
            Translations = new ObservableCollection<Translation>();
            _navigationService = navigationService;
            _userWordService = userWordService;
            _wordService = wordService;
            _popupService = popupService;
            GoToSearchWordCommand = new Command(async () => await GoToSearchBar());
            AddTranslationCommand = new Command(async () => await NavigateToAddTranslationAsync());
            DeleteTranslationCommand = new Command<Translation>(DeleteTranslation);


        }

        public override Task OnInitializedAsync()
        {
            AddWordButtonVisible = true;
            IsTranslationsVisible = false;
            StatusVisible = false;
            return Task.CompletedTask;
        }

        public override async Task OnAppearingAsync()
        {
            if (WordSelected)
            {
                IsTranslationsVisible = true;
                StatusVisible = true;
                AddWordButtonVisible = false;
                var word = await _wordService.GetAsync(Word);
                Word = word.Value;
                var translations = word.Translations.Split(new char[] { ',' }).ToList();
                foreach (var translation in translations)
                {
                    Translations.Add(new Translation { Value = translation, DeleteVisible = true, EditVisible = true });
                }

                WordSelected = false;
            }
        }

        private async Task NavigateToAddTranslationAsync()
        {
            var result = await _popupService.ShowAddWordTranslationPopup();
            if (result.Succeeded)
            {
                var translation = new Translation
                {
                    Value = result.Result,
                    DeleteVisible = true,
                    EditVisible = true
                };

                Translations.Add(translation);
            }
        }

        private async Task AddNewUserWordAsync()
        {
            //var userWord = new UserWord
            //{
            //       Word = Word,
            //       Status = Status,
            //       Translations = 
            //}
        }

        private void DeleteTranslation(Translation translation)
        {
            Translations.Remove(translation);
        }

        private async Task GoToSearchBar()
        {
            await _navigationService.NavigateToSearchWordViewAsync();
        }

      
    }
}
