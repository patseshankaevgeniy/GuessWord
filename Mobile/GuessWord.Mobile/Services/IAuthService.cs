using GuessWord.Mobile.Models;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public interface IAuthService
    {
        Task<SignInResultDto> TrySignInAsync(string login, string password);
        Task<SignUpResultDto> TrySignUpAsync(string login, string password, string username);
    }
}
