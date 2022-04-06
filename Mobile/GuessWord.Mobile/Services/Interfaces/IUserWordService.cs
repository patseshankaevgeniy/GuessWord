using GuessWord.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public interface IUserWordService
    {
        Task<List<UserWord>> GetAllAsync();
        Task<UserWord> GetAsync(int id);
        Task<UserWord> CreateAsync(string word);
        Task<UserWord> UpdateAsync(string status, int id);
        Task DeleteAsync(int id);

    }
}