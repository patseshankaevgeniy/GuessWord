using GuessWord.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.DataAccess.Repositories
{
    public interface IUserWordsRepository
    {
        Task<List<UserWord>> GetAllAsync(int userId);
        Task<UserWord> GetAsync(int id);
        Task<UserWord> CreateAsync(UserWord userWord);
        Task<UserWord> UpdateAsync(UserWord userWord);
        Task RemoveAsync(UserWord userWord);
    }
}
