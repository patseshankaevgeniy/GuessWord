using GuessWord.Mobile.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.Game.Controllers
{
    public interface ILevelsController
    {
        Task<LevelDto> GetLevelAsync();
    }
}