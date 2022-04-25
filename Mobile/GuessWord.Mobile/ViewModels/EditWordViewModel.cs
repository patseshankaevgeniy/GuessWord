using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Models;
using GuessWord.Mobile.Services;
using System.Threading.Tasks;

namespace GuessWord.Mobile.ViewModels
{
    public class EditWordViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IUserWordService _userWordService;

        public int WordId { get; set; }
        public UserWord EditWord { get; set; }

        public EditWordViewModel(INavigationService navigationService,
            IUserWordService userWordService)
        {
            _navigationService = navigationService;
            _userWordService = userWordService;
        }

        public override async Task OnInitializedAsync()
        {
            EditWord = await _userWordService.GetAsync(WordId);
        }

    }
}
