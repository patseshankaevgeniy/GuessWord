using GuessWord.Application.Levels.Models;
using System.Threading.Tasks;

namespace GuessWord.Application.Levels
{
    public interface ILevelsService
    {
        Task<LevelDto> GetLevelAsync();
    }
}