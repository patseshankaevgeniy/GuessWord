using GuessWord.Api.Models;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Services
{
    public interface ILevelsService
    {
        Task<LevelDto> GetLevelAsync();
    }
}