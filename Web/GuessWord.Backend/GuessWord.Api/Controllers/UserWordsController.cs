using GuessWord.BusinessLogic.Exceptions;
using GuessWord.BusinessLogic.Models;
using GuessWord.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Api.Controllers
{
    [Authorize(Policy = "user")]
    [ApiController]
    [Route("api/user-words")]
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordsService _userWordsService;

        public UserWordsController(IUserWordsService userWordsService)
        {
            _userWordsService = userWordsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserWordDto>>> GetAsync()
        {
            var userWords = await _userWordsService.GetUserWordsAsync();
            return Ok(userWords);
        }

        [HttpPost]
        public async Task<ActionResult<UserWordDto>> CreateAsync([FromBody] UserWordDto userWord)
        {
            if (userWord == null)
            {
                return BadRequest("Word is empty");
            }

            try
            {
                userWord = await _userWordsService.CreateUserWordAsync(userWord);
            }
            catch (ValidationExeption ex)
            {
                return BadRequest(ex.Message);
            }

            return Created($"api/user-words/{userWord.Id}", userWord);
        }
    }
}
