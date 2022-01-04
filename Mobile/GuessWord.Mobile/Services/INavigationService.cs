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
    }
}