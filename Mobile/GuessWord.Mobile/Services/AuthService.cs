using GuessWord.Mobile.Models;
using System;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBackendClient _backendClient;
        private readonly ICurrentUserService _userService;

        public AuthService(IBackendClient backendClient, ICurrentUserService userService)
        {
            _backendClient = backendClient;
            _userService = userService;
        }

        public async Task<SignInResultDto> TrySignInAsync(string login, string password)
        {
            var dto = new SignInDto { Login = login, Password = password };
            try
            {
                var result = await _backendClient.PostAsync<SignInResultDto, SignInDto>("auth/signIn", dto);
                if (result.Succeeded)
                {
                    _userService.AccessToken = result.Token;
                    _userService.IsSignedIn = true;
                }

                return result;
            }
            catch (Exception)
            {
                var result = new SignInResultDto { Succeeded = false, ErrorType = AuthErrorType.UnknowExeption };
                return result;
            }
        }

        public async Task<SignUpResultDto> TrySignUpAsync(string name, string login, string password)
        {
            var dto = new SignUpDto { Name = name, Login = login, Password = password };
            try
            {
                var result = await _backendClient.PostAsync<SignUpResultDto, SignUpDto>("auth/signUp", dto);
                if (result.Succeeded)
                {

                }
                return result;
            }
            catch (Exception)
            {
                var result = new SignUpResultDto { Succeeded = false, ErrorType = AuthErrorType.UnknowExeption };
                return result;
            }
        }
    }
}
