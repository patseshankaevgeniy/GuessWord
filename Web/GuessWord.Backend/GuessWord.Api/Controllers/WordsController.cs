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
    [Route("api/words")]
    public class WordsController : ControllerBase
    {
        private readonly IWordsService _wordsService;

        public WordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }

        [HttpGet(Name = "GetWords")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WordDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<List<WordDto>>> GetAllAsync()
        {
            var words = await _wordsService.GetAllAsync();
            if (words.Count == 0)
            {
                throw new NotFoundException("No words");
            }
            return Ok(words);
        }

        [HttpGet("word/{value}", Name = "GetWord")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WordDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<WordDto>> GetAsync(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ValidationException("Wrong id");
            }

            var wordDto = await _wordsService.GetAsync(value);
            if (wordDto == null)
            {
                throw new NotFoundException("");
            }

            return Ok(wordDto);
        }

        [HttpGet("search/{letter}", Name = "GetByLetter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WordDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<List<WordDto>>> GetByLetterAsync(string letter)
        {
            if (string.IsNullOrEmpty(letter))
            {
                throw new ValidationException("Wrong id");
            }

            var userWordDto = await _wordsService.GetByLetterAsync(letter);
            if (userWordDto == null)
            {
                throw new NotFoundException("");
            }

            return Ok(userWordDto);
        }

        [HttpPost(Name = "CreateWord")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WordDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<WordDto>> CreateAsync([FromBody] string wordValue)
        {
            if (string.IsNullOrEmpty(wordValue))
            {
                return BadRequest("Word is empty");
            }

            var word = await _wordsService.CreateAsync(wordValue);

            return Created($"api/words/{word.Id}", word);
        }
    }
}
