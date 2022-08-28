using GuessWord.Application.UserWords.Models;
using GuessWord.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Application.UserWords
{
    public interface IUserWordsService
    {
        Task<List<UserWordDto>> GetAllAsync();
        Task<UserWordDto> GetAsync(int id);
        Task<UserWordDto> CreateAsync(UserWordDto word);
        Task UpdateAsync(int id, UserWordPatchDto userWord);
        Task DeleteAsync(int id);

    }
}
