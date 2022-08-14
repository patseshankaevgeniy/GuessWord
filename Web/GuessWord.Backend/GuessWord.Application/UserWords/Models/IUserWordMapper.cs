using GuessWord.Domain.Entities;
using System.Threading.Tasks;

namespace GuessWord.Application.UserWords.Models
{
    public interface IUserWordMapper
    {
        UserWordDto Map(UserWord userWord);
    }
}