using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class CreateWordViewModel : BaseViewModel
    {
        private readonly IWordService _wordService;
        private readonly IUserWordService _UserWordService;
        private readonly INavigationService _navigationService;

        public IList<Models.Word> SearchWords { get; set; }
        public Models.Word SelectedWord { get; set; }
        public ObservableCollection<Models.Word> ViewWords { get; set; }
        public ObservableCollection<string> Translations { get; set; }


        public string Word { get; set; }
        public string NewTranslation { get; set; }
        public string TextWordButton { get; set; }
        public bool IsWordVisible { get; set; }

        public string Translation { get; set; }
        public bool IsTranslationsVisible { get; set; }
        public bool IsSearchBarVisible { get; set; }
        public bool IsAddNewTranslationVisible { get; set; }

        public Command GoToSearchBarCommand { get; set; }
        public Command OnSelectionChangedCommand { get; set; }
        public Command SearchNewWordCommand { get; set; }
        public Command ChooseWordCommand { get; set; }
        public Command AddNewTranslationCommand { get; set; }
        public Command SaveTranslationCommand { get; set; }

        public CreateWordViewModel(
                IWordService wordService,
                IUserWordService userWordService,
                INavigationService navigationService
                )
        {
            //    _wordService = wordService;
            //    _UserWordService = userWordService;
            //    _navigationService = navigationService;
            //    SearchWords = new List<Models.Word>();
            //    ViewWords = new ObservableCollection<Models.Word>();
            //    IsWordVisible = true;
            //    IsSearchBarVisible = false;
            //    IsAddNewTranslationVisible = false;
            //    TextWordButton = "add";
            //    SaveTranslationCommand = new Command(SaveTranslation);
            //    ChooseWordCommand = new Command(GoToChosenWord);
            //    GoToSearchBarCommand = new Command(GoToSearchBar);
            //    AddNewTranslationCommand = new Command(AddNewTranslation);
            //    SearchNewWordCommand = new Command<string>(async (letter) => await SearchNewWordAsync(letter));
        }

        //private void SaveTranslation()
        //{
        //    Translations.Add(NewTranslation);
        //}

        //private void AddNewTranslation()
        //{
        //    IsAddNewTranslationVisible = true;
        //}

        //private void GoToSearchBar()
        //{
        //    IsWordVisible = false;
        //    IsSearchBarVisible = true;
        //}

        //private void GoToChosenWord()
        //{

        //    IsSearchBarVisible = false;
        //    IsTranslationsVisible = true;
        //    IsWordVisible = true;
        //    TextWordButton = "edit";
        //    Word = SelectedWord.Value;

        //}

        //public async Task SearchNewWordAsync(string letter)
        //{

        //    if (letter.Length == 1)
        //    {
        //        var words = await _wordService.GetByLetterAsync(letter);
        //        SearchWords.Clear();
        //        ViewWords.Clear();

        //        foreach (var word in words)
        //        {
        //            SearchWords.Add(word);
        //            ViewWords.Add(word);
        //        }
        //    }
        //    if (letter.Length > 1)
        //    {
        //        ViewWords.Clear();

        //        for (int i = 0; i < SearchWords.Count; i++)
        //        {
        //            if (SearchWords[i].Value.ToLower().Contains(letter.ToLower()))
        //            {
        //                ViewWords.Add(SearchWords[i]);
        //            }
        //        }
        //    }
        //}

        //private async Task CreateAsync()
        //{
        //    if (!Validate()) { return; }

        //    var result = await _wordService.CreateAsync(Word);

        //}



        //public bool Validate()
        //{
        //    var isValid = true;
        //    return isValid;
        //}
    }
}
