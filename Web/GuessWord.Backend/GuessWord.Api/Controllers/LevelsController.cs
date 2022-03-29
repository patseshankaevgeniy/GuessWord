using GuessWord.Api.Models;
using GuessWord.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/levels")]
    public class LevelsController : ControllerBase
    {
        private readonly ILevelsService _levelsService;

        public LevelsController(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }

        [HttpGet(Name = "GetLevels")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LevelDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LevelDto>> GetAsync()
        {

            return await _levelsService.GetLevelAsync();
        }
    }
}
