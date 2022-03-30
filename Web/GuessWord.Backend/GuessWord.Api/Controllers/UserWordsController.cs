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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserWordDto>>> GetAllAsync()
        {
            var userWords = await _userWordsService.GetAllAsync();
            return Ok(userWords);
        }

        [HttpGet("{id}", Name = "GetUserWordById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWordDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserWordDto>> GetAsync(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Word is empty");
            }

            try
            {
                var userWordDto = await _userWordsService.GetAsync(id.Value);
                return Ok(userWordDto);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserWordDto>> UpdateAsync(int? id, [FromBody] UserWordDto userWordDto)
        {
            if (userWordDto == null || !id.HasValue)
            {
                return BadRequest("");
            }
            try
            {
                var updateWord = await _userWordsService.UpdateAsync(userWordDto, id.Value);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return StatusCode(403, ex.Message);
            }
            return Ok(userWordDto);
        }

        [HttpDelete("{id}")]
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
