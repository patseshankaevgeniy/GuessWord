using GuessWord.Application.Words.Models;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Application.Words
{
    public interface IWordsService
    {
        Task<List<WordDto>> FindAsync(string letter, string word);
        Task<List<Word>> GetOptionsWordsAsync();
        Task<List<WordWithTranslation>> GetWordsWithTranslation(int userId, WordStatus status);
        Task<WordDto> CreateAsync(WordDto word);
    }
}
