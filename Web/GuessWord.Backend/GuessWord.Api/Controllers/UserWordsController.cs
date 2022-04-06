using GuessWord.Api.Models;
using GuessWord.BusinessLogic.Exceptions;
using GuessWord.BusinessLogic.Models;
using GuessWord.BusinessLogic.Services.Interfaces;
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
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordsService _userWordsService;

        public UserWordsController(IUserWordsService userWordsService)
        {
            _userWordsService = userWordsService;
        }

        [HttpGet(Name = "GetUserWords")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserWordDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<List<UserWordDto>>> GetAllAsync()
        {
            var userWords = await _userWordsService.GetAllAsync();
            if (userWords.Count == 0)
            {
                throw new NotFoundException("No words");
            }
            return Ok(userWords);
        }

        [HttpGet("{id}", Name = "GetUserWord")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWordDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserWordDto>> GetAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("Wrong id");
            }

            var userWordDto = await _userWordsService.GetAsync(id.Value);
            if (userWordDto == null)
            {
                throw new NotFoundException("");
            }

            return Ok(userWordDto);
        }

        [HttpPost(Name = "CreateUserWord")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserWordDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserWordDto>> CreateAsync([FromBody] string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return BadRequest("Word is empty");
            }

            var userWord = await _userWordsService.CreateAsync(word);

            return Created($"api/user-words/{userWord.Id}", userWord);
        }

        [HttpPut("{id}", Name = "UpdateUserWord")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWordDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserWordDto>> UpdateAsync(int? id, [FromBody] int? status)
        {
            if (status == null || !id.HasValue)
            {
                return BadRequest("");
            }
            var updateWord = new UserWordDto();
            try
            {
                updateWord = await _userWordsService.UpdateAsync(status.Value, id.Value);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return StatusCode(403, ex.Message);
            }
            return Ok(updateWord);
        }

        [HttpDelete("{id}", Name = "DeleteUserWord")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id is empty");
            }
            try
            {
                await _userWordsService.DeleteAsync(id.Value);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return StatusCode(403, ex.Message);
            }

            return NoContent();
        }
    }
}
