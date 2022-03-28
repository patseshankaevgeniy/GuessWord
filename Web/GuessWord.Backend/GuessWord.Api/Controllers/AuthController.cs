using GuessWord.Api.Models;
using GuessWord.BusinessLogic.Exceptions;
using GuessWord.BusinessLogic.Models;
using GuessWord.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public ActionResult<SignInResultDto> SignIn([FromBody] SignInDto model)
        {
            if (model == null)
            {
                throw new ValidationException("Model is empty");
            }

            var signInResult = _authService.SignIn(model.Login, model.Password);
            return Ok(signInResult);

        }

        [HttpPost("signUp", Name = "SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignUpResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
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
