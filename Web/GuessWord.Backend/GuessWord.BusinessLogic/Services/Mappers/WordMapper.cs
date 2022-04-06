using GuessWord.BusinessLogic.Models;
using GuessWord.Domain.Entities;

namespace GuessWord.BusinessLogic.Services.Mappers
{
    public class WordMapper : IWordMapper
    {
        public WordDto Map(Word word)
        {
            var wordDto = new WordDto
            {
                Value = word.Value,
                UserWords = word.UserWords,
                Translations = word.Translations,
                Language = word.Language,
                Id = word.Id
            };

            return wordDto;
        }
    }
}
