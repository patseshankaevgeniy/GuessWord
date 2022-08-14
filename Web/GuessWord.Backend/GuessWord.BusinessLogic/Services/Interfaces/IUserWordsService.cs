using GuessWord.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Services.Interfaces
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
