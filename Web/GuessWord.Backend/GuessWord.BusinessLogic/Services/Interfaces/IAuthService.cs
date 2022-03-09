using GuessWord.BusinessLogic.Models;

namespace GuessWord.BusinessLogic.Services
{
    public interface IAuthService
    {
        SignInResultDto SignIn(string login, string password);
        SignUpResultDto SignUp(string name, string login, string password);

    }
}