using GuessWord.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class LevelsController : ILevelsController
    {
        private readonly IBackendClient _backendClient;

        public LevelsController(IBackendClient backendClient)
        {
            _backendClient = backendClient;
        }

        public async Task<LevelDto> GetLevelAsync()
        {
            return await _backendClient.GetAsync<LevelDto>("api/levels");
        }
    }
}
