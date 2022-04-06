using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Models;
using GuessWord.Mobile.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    [QueryProperty(nameof(WordId), nameof(WordId))]
    public class WordDetailsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWordService _userWordService;

        public int WordId { get; set; }

        public string Status { get; set; }
        public bool IsStatusVisible { get; set; }
        public bool IsEntryStatusVisible { get; set; }

        public string Word { get; set; }
        public string StatusButton { get; set; }
        public string TranslationButton { get; set; }

        public string Translations { get; set; }
        public bool IsTranslationVisible { get; set; }
        public bool IsEntryTranslationVisible { get; set; }

        public UserWord UserWord { get; set; }

        public Command GoBackCommand { get; set; }
        public Command ChangeStatusCommand { get; set; }
        public Command DeleteWordCommand { get; set; }
        public Command ChangeTranslationCommand { get; set; }
        public Command SaveWordCommand { get; set; }

        public WordDetailsViewModel(
            INavigationService navigationService,
           IUserWordService userWordService)
        {
            _navigationService = navigationService;
            _userWordService = userWordService;
            GoBackCommand = new Command(NavigateToBackPage);
            SaveWordCommand = new Command(SaveWordAsync);
            ChangeStatusCommand = new Command(ChangeStatus);
            DeleteWordCommand = new Command(DeleteAsync);
            ChangeTranslationCommand = new Command(ChangeTranslation);
        }

        public override async Task OnInitializedAsync()
        {
            IsStatusVisible = true;
            IsTranslationVisible = true;
            TranslationButton = "Change";
            StatusButton = "Change";
            UserWord = await _userWordService.GetAsync(WordId);
            Status = UserWord.Status;
            Translations = UserWord.Translations;
            Word = UserWord.Word;
        }

        private async void SaveWordAsync()
        {
            UserWord.Status = Status;
            var result = await _userWordService.UpdateAsync(Status, UserWord.Id);
        }

        private void DeleteAsync()
        {
            _userWordService.DeleteAsync(WordId);
        }

        private void ChangeTranslation()
        {
            if (IsTranslationVisible == false)
            {
                IsEntryTranslationVisible = false;
                IsTranslationVisible = true;
                TranslationButton = "Change";
                return;
            }

            IsTranslationVisible = false;
            IsEntryTranslationVisible = true;
            TranslationButton = "Cancel";
        }

        private async void ChangeStatus()
        {
            if (IsStatusVisible == false)
            {
                IsStatusVisible = true;
                IsEntryStatusVisible = false;
                StatusButton = "Change";
                return ;
            }

            IsStatusVisible = false;
            IsEntryStatusVisible = true;
            StatusButton = "Cancel";
        }

        private void NavigateToBackPage()
        {
            _navigationService.NavigateToUserWordAsync();
        }
    }
}