﻿using GuessWord.Mobile.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private const string SignedInKey = "signedIn";
        private const string UserToken = "userToken";
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
                    //var properties = Application.Current.Properties;
                    //properties.Add(SignedInKey, true);
                    //properties.Add(UserToken, result.Token);

                }

                return result;
            }
            catch (Exception)
            {
                var result = new SignInResultDto { Succeeded = false, ErrorType = AuthErrorType.UnknowExeption };
                return result;
            }
        }

        public Task<bool> CheckSignInAsync()
        {
            var properties = Application.Current.Properties;
            return Task.FromResult(properties.ContainsKey(SignedInKey) && (bool)properties[SignedInKey]);
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
