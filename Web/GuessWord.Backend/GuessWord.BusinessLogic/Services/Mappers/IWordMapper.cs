using GuessWord.BusinessLogic.Models;
using GuessWord.Domain.Entities;

namespace GuessWord.BusinessLogic.Services.Mappers
{
    public interface IWordMapper
    {
        WordDto Map(Word word);
    }
}