using GuessWord.Mobile.Clients;
using System.Threading.Tasks;


namespace GuessWord.Mobile.Services
{
    public class LevelsController : ILevelsController
    {
        private readonly IBackendClient _backendClient;
        private readonly IGuessWordApiClient _apiClient;

        public LevelsController(IBackendClient backendClient, IGuessWordApiClient apiClient)
        {
            _backendClient = backendClient;
            _apiClient = apiClient;
        }

        public async Task<LevelDto> GetLevelAsync()
        {
            return await _apiClient.GetLevelsAsync();
        }
    }
}
