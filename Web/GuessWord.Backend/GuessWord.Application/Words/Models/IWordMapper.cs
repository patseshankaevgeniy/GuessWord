using GuessWord.Domain.Entities;

namespace GuessWord.Application.Words.Models
{
    public interface IWordMapper
    {
        WordDto Map(Word word);
    }
}