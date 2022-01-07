using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Models;
using GuessWord.Mobile.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class PlayViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ILevelsController _levelsController;

        private int _currentStep;
        private Level _currentLevel;

        public string Step { get; set; }
        public string TargetWord { get; set; }
        public string FirstOption { get; set; }
        public string SecondOption { get; set; }
        public string ThirdOption { get; set; }

        public Command SelectFirstOptionCommand { get; set; }
        public Command SelectSecondOptionCommand { get; set; }
        public Command SelectThirdOptionCommand { get; set; }
        public Command GoToPreviosCommand { get; set; }
        public Command GoToNextCommand { get; set; }
        public Command FinishCommand { get; set; }


        public PlayViewModel(
            INavigationService navigationService,
            ILevelsController levelsController)
        {
            _navigationService = navigationService;
            _levelsController = levelsController;
            FinishCommand = new Command(NavigateToFinishGame);
           // GoToNextCommand = new Command(ChangeLevelToNext);

        }

        

        public async void NavigateToFinishGame()
        {
            await _navigationService.NavigateToFinishGameAsync();
        }

        public override async Task OnInitializedAsync()
        {
            var level = await _levelsController.GetLevelAsync();
            if (level != null)
            {
                _currentLevel = level;
                _currentStep = 1;
                Step = $"{_currentStep}/{_currentLevel.Count}";
                var step = level.Steps[_currentStep - 1];
                TargetWord = step.Target;
                FirstOption = step.Options[0].Word;
                SecondOption = step.Options[1].Word;
                ThirdOption  = step.Options[2].Word;
            }
        }
    }
}
