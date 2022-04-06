using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;

namespace GuessWord.BusinessLogic.Models
{
    public class WordDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public Language Language { get; set; }

        public ICollection<UserWord> UserWords { get; set; }
        public ICollection<WordTranslation> Translations { get; set; }
    }
}
