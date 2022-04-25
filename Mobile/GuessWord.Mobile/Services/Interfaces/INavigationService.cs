using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
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
        Task NavigateToUserWordAsync();
        Task NavigateToWordViewAsync(int wordId);
        Task NavigateToCreateWordViewAsync();
        Task NavigateToAddWordViewAsync();
        Task NavigateToSearchWordViewAsync();
        Task NavigateToEditWordViewAsync(int wordId);

    }
}