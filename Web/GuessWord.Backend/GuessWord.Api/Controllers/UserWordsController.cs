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
    }
}
