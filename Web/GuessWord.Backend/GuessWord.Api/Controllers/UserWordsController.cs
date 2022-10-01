using GuessWord.Application.Common.Models;
using GuessWord.Application.UserWords;
using GuessWord.Application.UserWords.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user-words")]
    [Produces("application/json")]
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordsService _userWordsService;

        public UserWordsController(IUserWordsService userWordsService)
        {
            _userWordsService = userWordsService;
        }

        [HttpGet(Name = "GetUserWords")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserWordDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<IEnumerable<UserWordDto>>> GetAsync()
        {
            var userWords = await _userWordsService.GetAllAsync();
            return Ok(userWords);
        }

        [HttpGet("{id}", Name = "GetUserWord")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWordDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<UserWordDto>> GetAsync(int id)
        {
            var userWordDto = await _userWordsService.GetAsync(id);
            return Ok(userWordDto);
        }

        [HttpPost(Name = "CreateUserWord")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserWordDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<UserWordDto>> CreateAsync(UserWordDto userWordDto)
        {
            userWordDto = await _userWordsService.CreateAsync(userWordDto);
            return Created($"api/user-words/{userWordDto.Id}", userWordDto);
        }

        [HttpPatch("{id}", Name = "UpdateUserWord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<UserWordDto>> UpdateAsync(int id, UserWordPatchDto userWordDto)
        {
            await _userWordsService.UpdateAsync(id, userWordDto);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteUserWord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _userWordsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
