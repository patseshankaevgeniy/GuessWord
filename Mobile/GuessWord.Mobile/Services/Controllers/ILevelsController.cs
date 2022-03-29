using GuessWord.Mobile.Clients;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public interface ILevelsController
    {
        Task<LevelDto> GetLevelAsync();
    }
}