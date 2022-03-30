using GuessWord.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Services.Interfaces
{
    public interface IUserWordsService
    {
        Task<List<UserWordDto>> GetAllAsync();
        Task<UserWordDto> GetAsync(int id);
        Task<UserWordDto> CreateAsync(string word);
        Task<UserWordDto> UpdateAsync(UserWordDto userWordDto, int id);
        Task DeleteAsync(int id);

    }
}
