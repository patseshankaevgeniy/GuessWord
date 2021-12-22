using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Models;
using GuessWord.Mobile.Services;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    [QueryProperty(nameof(Login), nameof(Login))]
    public class SignUpViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        public string UserName { get; set; }
        public string UserNameErrorText { get; set; }
        public bool IsUserNameErrorVisible { get; set; }

        public string Login { get; set; }
        public string LoginErrorText { get; set; }
        public bool IsLoginErrorVisible { get; set; }

        public string Password { get; set; }
        public string PasswordErrorText { get; set; }
        public bool IsPasswordErrorVisible { get; set; }

        public string ServerErrorText { get; set; }
        public bool IsServerErrorVisible { get; set; }


        public Command SignUpCommand { get; set; }

        public SignUpViewModel(IAuthService authService)
        {
            _authService = authService;

            SignUpCommand = new Command(SignUp);
        }

        private void SignUp()
        {
            if (!Validate())
            {
                return;
            }

            var result = _authService.TrySignUp(UserName, Login, Password);
            if (result.Success)
            {
                LoginErrorText = "Hello";
                IsLoginErrorVisible = true;
                return;
            }
            if (result.ErrorType == AuthErrorType.LoginAlreadyExists)
            {
                LoginErrorText = "This login is registered.";
                IsLoginErrorVisible = true;
            }
            if (result.ErrorType == AuthErrorType.BadUserName)
            {
                LoginErrorText = "This Name is registered.";
                IsLoginErrorVisible = true;
            }
            else if (result.ErrorType == AuthErrorType.WrongPassword)
            {
                //Wrong password
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

            IsServerErrorVisible = false;

            if (string.IsNullOrEmpty(UserName))
            {
                UserNameErrorText = "Name is empty.";
                IsUserNameErrorVisible = true;
                isValid = false;
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
