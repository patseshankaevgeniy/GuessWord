using GuessWord.Api.Models;
using GuessWord.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuessWord.Api.Controllers
{
    [ApiController]
    [Route("api/levels")]
    public class LevelsController : ControllerBase
    {
        private readonly ILevelsService _levelsService;

        public LevelsController(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }

        [HttpGet]
        public async Task<ActionResult<LevelDto>> GetAsync()
        {

            return await _levelsService.GetLevelAsync();
        }
    }
}
