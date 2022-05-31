using GuessWord.Mobile.Application.Auth.Models;
using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.Common.Services;
using System;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICurrentUserService _userService;
        private readonly IGuessWordApiClient _apiClient;

        public AuthService(
            ICurrentUserService userService,
            IGuessWordApiClient apiClient)
        {
            _userService = userService;
            _apiClient = apiClient;
        }

        public async Task<SignInResultDto> TrySignInAsync(string login, string password)
        {
            var dto = new SignInDto { Login = login, Password = password };
            try
            {
                var result = await _apiClient.SignInAsync(dto);
                if (result.Succeeded)
                {
                    _userService.AccessToken = result.Token;
                    _userService.IsSignedIn = true;
                }

                return result;
            }
            catch
            {
                var result = new SignInResultDto { Succeeded = false, ErrorType = (int)AuthErrorType.UnknowExeption };
                return result;
            }
        }

        public async Task<SignUpResultDto> TrySignUpAsync(string name, string login, string password)
        {
            var dto = new SignUpDto { Name = name, Login = login, Password = password };
            try
            {
                var result = await _apiClient.SignUpAsync(dto);
                if (result.Succeeded)
                {
                    
                }
                return result;
            }
            catch (Exception)
            {
                var result = new SignUpResultDto { Succeeded = false, ErrorType = (int)AuthErrorType.UnknowExeption };
                return result;
            }
        }
    }
}
