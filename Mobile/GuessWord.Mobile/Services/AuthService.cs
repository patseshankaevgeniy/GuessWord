using GuessWord.Mobile.Models;

namespace GuessWord.Mobile.Services
{
    public class AuthService : IAuthService
    {
        public SignInResult TrySignIn(string login, string password)
        {
            var result = new SignInResult { Succeeded = true };
            return result;

        }

        public SignUpResult TrySignUp(string login, string password, string username)
        {
            throw new System.NotImplementedException();
        }
    }
}
