using GuessWord.Application.Words.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Application.Words
{
    public interface IWordsService
    {
        Task<List<WordDto>> FindAsync(string letter = null);
        Task<WordDto> CreateAsync(WordDto word);
    }
}
