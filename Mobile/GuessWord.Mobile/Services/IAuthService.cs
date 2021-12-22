using GuessWord.Mobile.Models;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public interface IAuthService
    {
        SignInResult TrySignIn(string login, string password);
        SignUpResult TrySignUp(string login, string password, string username);
        Task<bool> CheckSignInAsync();
    }
}
