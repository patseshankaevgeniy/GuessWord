using GuessWord.Application.Auth;
using GuessWord.Application.Auth.Models;
using GuessWord.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuessWord.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-in", Name = "SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignInResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<SignInResultDto>> SignInAsync(SignInDto signInDto)
        {
            var signInResultDto = await _authService.SignInAsync(signInDto);
            return Ok(signInResultDto);
        }

        [HttpPost("sign-up", Name = "SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignUpResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<SignUpResultDto>> SignUpAsync(SignUpDto signUpDto)
        {
            var signUpResultDto = await _authService.SignUpAsync(signUpDto);
            return Ok(signUpResultDto);
        }
    }
}
