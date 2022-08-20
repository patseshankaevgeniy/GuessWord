using GuessWord.Domain.Enums;
using System.Collections.Generic;

namespace GuessWord.Domain.Entities
{
    public class Word : BaseEntity
    {
        public string Value { get; set; }
        public Language Language { get; set; }

        public ICollection<UserWord> UserWords { get; set; }
        public ICollection<WordTranslation> Translations { get; set; }
    }
}
