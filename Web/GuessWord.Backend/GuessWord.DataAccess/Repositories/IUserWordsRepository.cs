using GuessWord.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.DataAccess.Repositories
{
    public interface IUserWordsRepository
    {
        Task<List<UserWord>> GetUserWordsAsync(int userId);
        bool AddUserWord(Word word);
        bool DeleteUserWord(Word word);
        bool UpdateUserWord(Word word);
    }
}
