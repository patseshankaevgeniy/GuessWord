using GuessWord.Mobile.Clients;
using System.Threading.Tasks;


namespace GuessWord.Mobile.Services
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
            return await _apiClient.GetLevelsAsync();
        }
    }
}
