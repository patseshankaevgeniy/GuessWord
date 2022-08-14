using GuessWord.Application.Auth;
using GuessWord.Application.Auth.Models;
using GuessWord.Application.Common.Models;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("signIn", Name = "SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignInResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public ActionResult<SignInResultDto> SignIn(SignInDto model)
        {
            var signInResult = _authService.SignIn(model.Login, model.Password);
            return Ok(signInResult);
        }

        [HttpPost("signUp", Name = "SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignUpResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public ActionResult<SignUpResultDto> SignUp(SignUpDto model)
        {
            var signUpResult = _authService.SignUp(model.Name, model.Login, model.Password);
            return Ok(signUpResult);
        }
    }
}
