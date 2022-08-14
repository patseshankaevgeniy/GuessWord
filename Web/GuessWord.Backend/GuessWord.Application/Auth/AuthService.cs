using GuessWord.Application.Auth.Models;
using GuessWord.Application.Common.Interfaces;
using GuessWord.Application.Common.Interfaces.Repositories;

namespace GuessWord.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        private const int DublicateResult = -1;

        public AuthService(
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public SignInResultDto SignIn(string login, string password)
        {
            var signInResult = new SignInResultDto() { Succeeded = true };
            var user = _userRepository.GetByLogin(login);
            if (user == null)
            {
                signInResult.Succeeded = false;
                signInResult.ErrorType = (int)AuthErrorType.UserNotFound;
                return signInResult;
            }

            if (user.Login == login && user.Password != password)
            {
                signInResult.Succeeded = false;
                signInResult.ErrorType = (int)AuthErrorType.WrongPassword;
                return signInResult;
            }

            signInResult.Token = _tokenService.BuildToken(user);

            return signInResult;
        }

        public SignUpResultDto SignUp(string name, string login, string password)
        {
            var signUpResult = new SignUpResultDto() { Succeeded = true };
            var userId = _userRepository.AddNewUser(name, login, password);
            if (userId == DublicateResult)
            {
                signUpResult.Succeeded = false;
                signUpResult.ErrorType = (int)AuthErrorType.LoginAlreadyExists;
                return signUpResult;
            }

            return signUpResult;
        }
    }
}
