using GuessWord.Domain.Entities;
using System.Linq;

namespace GuessWord.Application.Words.Models
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
