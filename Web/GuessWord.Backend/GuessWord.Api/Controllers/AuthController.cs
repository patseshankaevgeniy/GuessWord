using GuessWord.Api.Models;
using GuessWord.BusinessLogic.Models;
using GuessWord.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessWord.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signIn")]
        public ActionResult<SignInResultDto> SignIn([FromBody] SignInDto model)
        {
            if (model == null)
            {
                return BadRequest("Model is empty");
            }

            var signInResult = _authService.SignIn(model.Login, model.Password);

            return Ok(signInResult);
        }

        [HttpPost("signUp")]
        public ActionResult<SignUpResultDto> SignUp([FromBody] SignUpDto model)
        {
            if (model == null)
            {
                return BadRequest("Model is empty");
            }

            var signUpResult = _authService.SignUp(model.Name, model.Login, model.Password);

            return Ok(signUpResult);
        }
    }
}
