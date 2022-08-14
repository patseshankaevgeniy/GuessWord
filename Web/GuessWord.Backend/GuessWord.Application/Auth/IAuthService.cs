using GuessWord.Application.Auth.Models;

namespace GuessWord.Application.Auth
{
    public interface IAuthService
    {
        SignInResultDto SignIn(string login, string password);
        SignUpResultDto SignUp(string name, string login, string password);

    }
}