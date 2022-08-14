using GuessWord.Application.Common.Models;
using GuessWord.Application.Words;
using GuessWord.Application.Words.Models;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WordDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<IEnumerable<WordDto>>> GetAsync(string letter, string word)
        {
            var words = await _wordsService.FindAsync(letter, word);
            return Ok(words);
        }

        [HttpPost(Name = "CreateWord")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WordDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<WordDto>> CreateAsync(WordDto wordDto)
        {
            wordDto = await _wordsService.CreateAsync(wordDto);
            return Created($"api/words/{wordDto.Id}", wordDto);
        }
    }
}
