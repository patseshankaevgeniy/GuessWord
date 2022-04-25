using GuessWord.BusinessLogic.Models;
using GuessWord.Domain.Entities;
using System.Linq;

namespace GuessWord.BusinessLogic.Services.Mappers
{
    public class WordMapper : IWordMapper
    {
        public WordDto Map(Word word)
        {
            var wordDto = new WordDto
            {
                Value = word.Value,
                Translations = word.Translations.Select(x => x.Translation.Value)
                        .ToList(),
                Id = word.Id
            };

            return wordDto;
        }
    }
}
