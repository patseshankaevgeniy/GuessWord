using GuessWord.Mobile.Models;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public interface ILevelsController
    {
        Task<Level> GetLevelAsync();
    }
}