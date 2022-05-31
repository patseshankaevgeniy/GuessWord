using GuessWord.Mobile.Application.Auth.Models;
using GuessWord.Mobile.Application.Auth.Services;
using GuessWord.Mobile.Application.Common;
using GuessWord.Mobile.Application.Common.Interfaces;
using Xamarin.Forms;

namespace GuessWord.Mobile.Application.Auth.ViewModels
{
    [QueryProperty(nameof(Login), nameof(Login))]
    public class SignUpViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly INavigationService _navigationService;

        public string UserName { get; set; }
        public string UserNameErrorText { get; set; }
        public bool IsUserNameErrorVisible { get; set; }

        public string Login { get; set; }
        public string LoginErrorText { get; set; }
        public bool IsLoginErrorVisible { get; set; }

        public string Password { get; set; }
        public string PasswordErrorText { get; set; }
        public bool IsPasswordErrorVisible { get; set; }

        public Command SignUpCommand { get; set; }

        public SignUpViewModel(
            IAuthService authService,
            INavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
            SignUpCommand = new Command(SignUp);
        }

        private async void NavigateToSignInAfterRegistre()
        {
            await _navigationService.NavigateToSignInAfterRegistreAsync(Login, Password);
        }

        private async void SignUp()
        {
            if (!Validate())
            {
                return;
            }

            var result = await _authService.TrySignUpAsync(UserName, Login, Password);
            if (result.Succeeded)
            {
                NavigateToSignInAfterRegistre();
                return;
            }
            if (result.ErrorType == (int)AuthErrorType.LoginAlreadyExists)
            {
                LoginErrorText = "This login is registered.";
                IsLoginErrorVisible = true;
            }
            if (result.ErrorType == (int)AuthErrorType.BadUserName)
            {
                LoginErrorText = "This Name is registered.";
                IsLoginErrorVisible = true;
            }
            else if (result.ErrorType == (int)AuthErrorType.WrongPassword)
            {
                PasswordErrorText = "Invalid password.";
                IsPasswordErrorVisible = true;
                return;
            }
            else
            {
                LoginErrorText = "Sorry, server error.";
                IsLoginErrorVisible = true;
                return;
            }
        }

        public bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrEmpty(UserName))
            {
                UserNameErrorText = "Name is empty.";
                IsUserNameErrorVisible = true;
                isValid = false;
            }
            else
            {
                IsUserNameErrorVisible = false;
            }

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
