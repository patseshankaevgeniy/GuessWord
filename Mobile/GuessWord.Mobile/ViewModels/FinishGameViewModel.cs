using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;

namespace GuessWord.Mobile.ViewModels
{
    public class FinishGameViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ILevelsController _levelsController;

        public FinishGameViewModel(
            INavigationService navigationService,
            ILevelsController levelsController)
        {
            _navigationService = navigationService;
            _levelsController = levelsController;
        }
    }
}