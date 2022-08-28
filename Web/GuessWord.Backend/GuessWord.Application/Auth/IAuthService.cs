using GuessWord.Application.Auth.Models;
using System.Threading.Tasks;

namespace GuessWord.Application.Auth
{
    public interface IAuthService
    {
        Task<SignInResultDto> SignInAsync(SignInDto signInDto);
        Task<SignUpResultDto> SignUpAsync(SignUpDto signUpDto);
    }
}