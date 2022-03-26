using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.DataAccess.Repositories
{
    public interface IWordsRepository
    {
        List<WordWithTranslation> GetWordsWithTranslation(int userId, WordStatus status);
        Task<List<Word>> GetOptionsWordsAsync();
        Task<Word> GetByNameAsync(string value);
        Task<Word> CreateAsync(Word newWord);
        
    }
}