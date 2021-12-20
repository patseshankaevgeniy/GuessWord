using GuessWord.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuessWord.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        [HttpPost("signIn")]
        public ActionResult SignIn([FromBody]SignInModel model)
        {
            var result = new Models.SignInResult();

            if (model.UserName == "Dzhon" && model.Password == "1234")
            {
                result.Succeeded = true;
            }
            else if (model.UserName != "Dzhon")
            {
                result.Succeeded = false;
                result.ErrorType = AuthErrorType.UserNotFound;
            }
            else if (model.Password != "1234")
            {
                result.Succeeded = false;
                result.ErrorType = AuthErrorType.WrongPassword;
            }
            else
            {
                result.Succeeded = false;
                result.ErrorType = AuthErrorType.UnknowExeption;
            }

            return Ok(result);
        }
    }
}
