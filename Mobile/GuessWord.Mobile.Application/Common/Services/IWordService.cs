using GuessWord.Mobile.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.Common.Services
{
    public interface IWordService
    {
        Task<List<Word>> GetAllAsync();
        Task<List<Word>> GetByLetterAsync(string letter);
        Task<Word> GetAsync(string value);
        Task<Word> CreateAsync(string word);
        Task DeleteAsync(int id);

    }
}