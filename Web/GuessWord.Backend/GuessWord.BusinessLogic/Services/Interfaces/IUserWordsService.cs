using GuessWord.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Services.Interfaces
{
    public interface IUserWordsService
    {
        Task<List<UserWordDto>> GetUserWordsAsync();
    }
}
