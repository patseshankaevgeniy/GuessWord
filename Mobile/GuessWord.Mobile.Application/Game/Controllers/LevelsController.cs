using GuessWord.Mobile.Application.Common.Interfaces;
using System.Threading.Tasks;


namespace GuessWord.Mobile.Application.Game.Controllers
{
    public class LevelsController : ILevelsController
    {
        private readonly IGuessWordApiClient _apiClient;

        public LevelsController(IGuessWordApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<LevelDto> GetLevelAsync()
        {
            var level = await _apiClient.GetLevelsAsync();
            return level;
        }
    }
}
