using GuessWord.Mobile.Models;

namespace GuessWord.Mobile.Services
{
    public interface IAuthService
    {
        SignInResult TrySignIn(string login, string password);
        SignUpResult TrySignUp(string login, string password, string username);
    }
}
