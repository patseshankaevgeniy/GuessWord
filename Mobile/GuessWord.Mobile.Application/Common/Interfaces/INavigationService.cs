using GuessWord.Mobile.Application.Common.Models;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.Common.Interfaces
{
    public interface INavigationService
    {
        Task NavigateToSignInAsync();
        Task NavigateToSignInAfterRegistreAsync(string login, string password);
        Task NavigateToSignUpAsync(string login);
        Task NavigateBackAsync();
        Task NavigateToMainAsync();
        Task NavigateToPlayViewAsync();
        Task NavigateToFinishGameAsync();
        Task NavigateToUserWordAsync(bool changedWord = false);
        Task NavigateToAddWordViewAsync();
        Task NavigateToAddWordViewAfterSearchAsync(string word);
        Task NavigateToSearchWordViewAsync();
        Task NavigateToEditWordViewAsync(int wordId);
        Task NavigateBackFromSearchAsync();
    }
}