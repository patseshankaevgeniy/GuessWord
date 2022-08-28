using GuessWord.Application.Levels;
using GuessWord.Application.Levels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuessWord.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/levels")]
    [Produces("application/json")]
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
            var level = await _levelsService.GetLevelAsync();
            return level;
        }
    }
}
