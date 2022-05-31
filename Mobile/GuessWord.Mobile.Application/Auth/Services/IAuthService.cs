using GuessWord.Mobile.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.Auth.Services
{
    public interface IAuthService
    {
        Task<SignInResultDto> TrySignInAsync(string login, string password);
        Task<SignUpResultDto> TrySignUpAsync(string login, string password, string username);
    }
}
