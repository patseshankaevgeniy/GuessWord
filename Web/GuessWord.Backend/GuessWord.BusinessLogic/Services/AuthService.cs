using GuessWord.BusinessLogic.Models;
using GuessWord.DataAccess;
using GuessWord.DataAccess.Repositories;

namespace GuessWord.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
