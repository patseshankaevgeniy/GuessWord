using GuessWord.Mobile.Application.UserWords.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.UserWords.Services
{
    public interface IUserWordService
    {
        Task<List<UserWord>> GetAllAsync();
        Task<UserWord> GetAsync(int id);
        Task<UserWord> CreateAsync(UserWord word);
        Task<UserWord> UpdateAsync(UserWord userWord);
        
        Task DeleteAsync(int id);

    }
}