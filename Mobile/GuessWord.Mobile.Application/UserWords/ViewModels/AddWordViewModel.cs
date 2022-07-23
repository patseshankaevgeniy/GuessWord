using GuessWord.Mobile.Application.Common;
using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.Common.Models;
using GuessWord.Mobile.Application.Common.Services;
using GuessWord.Mobile.Application.UserWords.Models;
using GuessWord.Mobile.Application.UserWords.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.Application.UserWords.ViewModels
{
    public class AddWordViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWordService _userWordService;
        private readonly IWordService _wordService;
        private readonly IPopupService _popupService;
        private readonly UserWordsController _userWordsController;

        public Word UserWord { get; set; }
        public bool AddWordButtonVisible { get; set; }

        public string Word { get; set; }
        public bool AddWordVisible { get; set; }
        public bool IsRefreshing { get; set; }
        public Command GoToSearchWordCommand { get; set; }
        public Command AddNewUserWordCommand { get; set; }

        public ObservableCollection<Translation> Translations { get; set; }
        public bool IsTranslationsVisible { get; set; }
        public Command AddTranslationCommand { get; set; }
        public Command SaveNewTranslationCommand { get; set; }
        public Command DeleteTranslationCommand { get; set; }

        public string Status { get; set; }
        public bool StatusVisible { get; set; }
        public Command GoBackCommand { get; set; }

        public AddWordViewModel(INavigationService navigationService,
                                IUserWordService userWordService,
                                IWordService wordService,
                                IPopupService popupService,
                                UserWordsController userWordsController)
        {
            Translations = new ObservableCollection<Translation>();
            _navigationService = navigationService;
            _userWordService = userWordService;
            _wordService = wordService;
            _popupService = popupService;
            _userWordsController = userWordsController;
            GoToSearchWordCommand = new Command(async () => await GoToSearchBar());
            GoBackCommand = new Command(async () => await GoBackAsync());
            AddTranslationCommand = new Command(async () => await NavigateToAddTranslationAsync());
            DeleteTranslationCommand = new Command<Translation>(DeleteTranslation);


        }

        private async Task GoBackAsync()
        {
            var result = await _popupService.ShowSaveConfirmationPopupAsync();
            if (result.Succeeded)
            {
                await SaveUserWordAsync();
            }

            await _navigationService.NavigateToUserWordAsync(changedWord: result.Succeeded);
        }

        public override Task OnInitializedAsync()
        {

            AddWordButtonVisible = true;
            IsTranslationsVisible = false;
            StatusVisible = false;
            return Task.CompletedTask;
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

        private async Task SaveUserWordAsync()
        {
            var userWord = new UserWord
            {

                Status = Status,
                Word = Word,
                Translations = string.Join(',', Translations.Select(x => x.Value))
            };

            await _userWordService.CreateAsync(userWord);
        }

        private void DeleteTranslation(Translation translation)
        {
            Translations.Remove(translation);
        }

        private async Task GoToSearchBar()
        {
            StatusVisible = true;
            IsTranslationsVisible = true;
            await _navigationService.NavigateToSearchWordViewAsync();
        }

        public async void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (!query.Any())
            {
                StatusVisible = false;
                IsTranslationsVisible = false;
                return;
            }
            
            Word = query.First(x => x.Key == nameof(Word)).Value.Replace("%20", "");
            AddWordButtonVisible = false;
            StatusVisible = true;
            IsTranslationsVisible = true;

            var word = await _wordService.GetAsync(Word);
            var translations = word.Translations.Split(new char[] { ',' }).ToList();
            foreach (var translation in translations)
            {
                Translations.Add(new Translation { Value = translation, DeleteVisible = true, EditVisible = true });
            }
        }
    }
}
