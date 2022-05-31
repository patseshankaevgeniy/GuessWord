using GuessWord.Mobile.Application.Common;
using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.UserWords.Models;
using GuessWord.Mobile.Application.UserWords.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.Application.UserWords.ViewModels
{
    [QueryProperty(nameof(WordId), nameof(WordId))]
    [QueryProperty(nameof(ChangedTranslation), nameof(ChangedTranslation))]
    public class EditWordViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWordService _userWordService;
        private readonly IUserWordEditService _userWordEditService;
        private readonly IPopupService _popupService;

        public int WordId { get; set; }
        public bool IsSaveVisible { get; set; }

        public string NewTranslation { get; set; }
        public string TextEditButton { get; set; }


        public string Word { get; set; }
        public Command RemoveUserWordCommand { get; set; }
        public Command SaveUserWordCommand { get; set; }


        public ObservableCollection<Translation> Translations { get; set; }
        public Translation SelectedTranslation { get; set; }
        public bool ChangedTranslation { get; set; }
        public bool TranslationVisible { get; set; }
        public bool TranslationEditorVisible { get; set; }
        public Command AddTranslationCommand { get; set; }
        public Command EditTranslationCommand { get; set; }
        public Command DeleteTranslationCommand { get; set; }
        public Command GoBackCommand { get; set; }


        public string Status { get; set; }
        public Command ChangeStatusCommand { get; set; }

        public EditWordViewModel(
            INavigationService navigationService,
            IUserWordService userWordService,
            IUserWordEditService userWordEditService,
            IPopupService popupService)
        {
            _navigationService = navigationService;
            _userWordService = userWordService;
            _userWordEditService = userWordEditService;
            _popupService = popupService;

            Translations = new ObservableCollection<Translation>();
            RemoveUserWordCommand = new Command(async () => await DeleteUserWordAsync());
            SaveUserWordCommand = new Command(async () => await SaveUserWordAsync());
            AddTranslationCommand = new Command(async () => await NavigateToAddTranslationAsync());
            EditTranslationCommand = new Command<Translation>(async (SelectedTranslation) => await NavigateToEditTranslationAsync(SelectedTranslation));
            DeleteTranslationCommand = new Command<Translation>(DeleteTranslation);
            GoBackCommand = new Command(async () => await GoBackAsync());

            ChangeStatusCommand = new Command(async () => await ChangeStatusAsync());
        }

        private async Task GoBackAsync()
        {
            if (IsSaveVisible)
            {
                var result = await _popupService.ShowSaveConfirmationPopupAsync();
                if (result.Succeeded)
                {
                    await SaveUserWordAsync();
                    await _navigationService.NavigateToUserWordAsync(changedWord: true);
                    return;
                }
            }

            await _navigationService.NavigateToUserWordAsync();
        }

        public override async Task OnInitializedAsync()
        {
            if (WordId <= 0)
            {
                throw new ArgumentNullException("Word id is not valid");
            }

            var editedWord = await _userWordEditService.GetAsync(WordId);
            SetupWord(editedWord);

            IsSaveVisible = false;
        }

        public override async Task OnAppearingAsync()
        {
            if (ChangedTranslation)
            {
                var editWord = await _userWordEditService.GetAsync(WordId);
                SetupWord(editWord);
                ChangedTranslation = false;
            }
        }

        private async Task ChangeStatusAsync()
        {
            var result = await _popupService.ShowChangeStatusPopup();
            if (result.Succeeded)
            {
                Status = result.Result;
                IsSaveVisible = true;
            }
        }

        private async Task SaveUserWordAsync()
        {
            var userWord = new UserWord
            {
                Id = WordId,
                Status = Status,
                Word = Word,
                Translations = string.Join(',', Translations.Select(x => x.Value))
            };

            await _userWordService.UpdateAsync(userWord);
        }

        private async Task NavigateToEditTranslationAsync(Translation translation)
        {
            var result = await _popupService.ShowEditWordTranslationPopup(translation.Value);
            if (result.Succeeded)
            {
                var index = Translations.IndexOf(translation);
                Translations.Remove(translation);
                translation.Value = result.Result;
                Translations.Insert(index, translation);
                IsSaveVisible = true;
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
                IsSaveVisible = true;
            }
        }
        private void SetupWord(UserWordEdit wordEdit)
        {
            TextEditButton = "Edit";


            Word = wordEdit.Word;
            Status = wordEdit.Status;
            Translations = new ObservableCollection<Translation>(wordEdit.Translations);
        }



        private void DeleteTranslation(Translation translation)
        {
            Translations.Remove(translation);
            IsSaveVisible = true;
        }

        public async Task DeleteUserWordAsync()
        {
            var result = await _popupService.ShowRemoveWordTranslationPopup();
            if (result.Result)
            {
                await _userWordService.DeleteAsync(WordId);
                await _navigationService.NavigateToUserWordAsync(changedWord: true);
            }
        }

    }
}
