using GuessWord.Mobile.Application.UserWords.Models;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.UserWords.Services
{
    public interface IUserWordEditService
    {
        Task<UserWordEdit> GetAsync(int id);
    }
}