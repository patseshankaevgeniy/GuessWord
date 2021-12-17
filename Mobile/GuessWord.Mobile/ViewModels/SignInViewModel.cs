using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Models;
using GuessWord.Mobile.Services;
using System;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

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

        public SignInViewModel(IAuthService authService)
        {
            _authService = authService;

            GoToSignUpCommand = new Command(NavigateToSignUp);
            SignInCommand = new Command(SignIn);
        }

        private void NavigateToSignUp()
        {

        }

        private void SignIn()
        {
            if (!Validate())
            {
                return;
            }

            var result = _authService.TrySignIn(Login, Password);
            if (result.Succeeded)
            {

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
