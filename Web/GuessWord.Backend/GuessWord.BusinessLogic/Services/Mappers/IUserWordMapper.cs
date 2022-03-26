using GuessWord.BusinessLogic.Models;
using GuessWord.Domain.Entities;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Services.Mappers
{
    public interface IUserWordMapper
    {
        UserWordDto Map(UserWord userWord);
    }
}