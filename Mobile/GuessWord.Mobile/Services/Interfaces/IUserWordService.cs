using GuessWord.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public interface IUserWordService
    {
        Task<List<UserWord>> GetAllAsync();
        Task<UserWord> GetAsync(int id);
        Task<UserWord> CreateAsync(UserWord userWord);
        Task<UserWord> UpdateAsync(UserWord userWord, int id);
        Task DeleteAsync(int id);

    }
}