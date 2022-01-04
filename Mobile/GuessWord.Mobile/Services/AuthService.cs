using GuessWord.Mobile.Models;
using System;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBackendClient _backendClient;

        public AuthService(IBackendClient backendClient)
        {
            _backendClient = backendClient;
        }

        public async Task<SignInResultDto> TrySignInAsync(string login, string password)
        {
            var dto = new SignInDto { Login = login, Password = password };
            try
            {
                return await _backendClient.PostAsync<SignInResultDto, SignInDto>("auth/signIn", dto);

            }
            catch (Exception)
            {
                var result = new SignInResultDto { Succeeded = false, ErrorType = AuthErrorType.UnknowExeption };
                return result;
            }
        }

        public Task<bool> CheckSignInAsync()
        {
            var properties = Xamarin.Forms.Application.Current.Properties;
            if (properties.ContainsKey("login") && properties.ContainsKey("password"))
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
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
