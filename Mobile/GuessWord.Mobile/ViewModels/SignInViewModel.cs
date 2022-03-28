using GuessWord.Mobile.Clients;
using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Models;
using GuessWord.Mobile.Services;
using System;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    [QueryProperty(nameof(Login), nameof(Login))]
    [QueryProperty(nameof(Password), nameof(Password))]
    public class SignInViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly INavigationService _navigationService;

        public string Login { get; set; }
        public string LoginErrorText { get; set; }
        public bool IsLoginErrorVisible { get; set; }

        public string Password { get; set; }
        public string PasswordErrorText { get; set; }
        public bool IsPasswordErrorVisible { get; set; }

        public string ServerErrorText { get; set; }
        public bool IsServerErrorVisible { get; set; }

        public Command GoToSignUpCommand { get; set; }

        public Command SignInCommand { get; set; }

        public SignInViewModel(
            IAuthService authService, 
            INavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
            GoToSignUpCommand = new Command(NavigateToSignUp);
            SignInCommand = new Command(SignIn);
        }

        private async void NavigateToSignUp()
        {
            await _navigationService.NavigateToSignUpAsync(Login);
        }

        private async void SignIn()
        {
            if (!Validate())
            {
                return;
            }

            var result = await _authService.TrySignInAsync(Login, Password);
            if (result.Succeeded)
            {
                await _navigationService.NavigateToMainAsync();
                return;
            }

            if (result.ErrorType == AuthErrorType.UserNotFound)
            {
                LoginErrorText = "This login is not registered.";
                IsLoginErrorVisible = true;
            }
            else if (result.ErrorType == AuthErrorType.WrongPassword)
            {
                PasswordErrorText = "Invalid password.";
                IsPasswordErrorVisible = true;
                return;
            }

            else
            {
                ServerErrorText = "Sorry, server error.";
                IsServerErrorVisible = true;
                return;
            }

        }

        public bool Validate()
        {
            var isValid = true;

            IsServerErrorVisible = false;

            if (string.IsNullOrEmpty(Login))
            {
                LoginErrorText = "Login is empty.";
                IsLoginErrorVisible = true;
                isValid = false;
            }
            else
            {
                IsLoginErrorVisible = false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                PasswordErrorText = "Password is empty.";
                IsPasswordErrorVisible = true;
                isValid = false;
            }
            else
            {
                IsPasswordErrorVisible = false;
            }

            return isValid;
        }
    }
}
