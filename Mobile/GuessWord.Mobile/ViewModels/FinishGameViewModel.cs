using GuessWord.Mobile.Clients;
using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class FinishGameViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ILevelsController _levelsController;

        private int _currentStep;
        private LevelDto _currentLevel;
        private int _levelNumber;

        public string TargetWord;
        public bool ResultTargetWord;

        public Command GoBackCommand { get; set; }
        public Command RepeatCommand { get; set; }

        public FinishGameViewModel(
            INavigationService navigationService,
            ILevelsController levelsController)
        {
            _navigationService = navigationService;
            _levelsController = levelsController;
            GoBackCommand = new Command(NavigateToBackPage);
            RepeatCommand = new Command(NavigateToPlayView);
        }

        private async void NavigateToPlayView()
        {
            await _navigationService.NavigateToPlayViewAsync();
        }

        private async void NavigateToBackPage()
        {
            await _navigationService.NavigateToMainAsync();
        }
    }
}