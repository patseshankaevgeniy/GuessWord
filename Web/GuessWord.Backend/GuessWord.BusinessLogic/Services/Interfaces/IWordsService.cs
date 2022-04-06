using GuessWord.BusinessLogic.Models;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Services.Interfaces
{
    public interface IWordsService
    {
        Task<List<WordDto>> GetAllAsync();
        Task<WordDto> GetAsync(string value);
        Task<List<WordDto>> GetByLetterAsync(string letter);
        Task<List<Word>> GetOptionsWordsAsync();
        Task<List<WordWithTranslation>> GetWordsWithTranslation(int userId, WordStatus status);
        Task<WordDto> CreateAsync(string word);
    }
}
