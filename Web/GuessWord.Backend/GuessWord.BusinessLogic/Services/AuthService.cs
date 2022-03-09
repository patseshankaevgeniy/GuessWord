using GuessWord.BusinessLogic.Models;
using GuessWord.BusinessLogic.Services.Interfaces;
using GuessWord.DataAccess;
using GuessWord.DataAccess.Repositories;

namespace GuessWord.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

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
                signInResult.ErrorType = AuthErrorType.UserNotFound;
                return signInResult;
            }

            if (user.Login == login && user.Password != password)
            {
                signInResult.Succeeded = false;
                signInResult.ErrorType = AuthErrorType.WrongPassword;
                return signInResult;
            }

            signInResult.Token = _tokenService.BuildToken(user);

            return signInResult;
        }

        public SignUpResultDto SignUp(string name, string login, string password)
        {
            var signUpResult = new SignUpResultDto() { Succeeded = true };
            var userId = _userRepository.AddNewUser(name, login, password);
            if (userId == Constants.DublicateResult)
            {
                signUpResult.Succeeded = false;
                signUpResult.ErrorType = AuthErrorType.LoginAlreadyExists;
                return signUpResult;
            }

            return signUpResult;
        }
    }
}
