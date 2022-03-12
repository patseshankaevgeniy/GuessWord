using System.Collections.Generic;

namespace GuessWord.Domain.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int Language { get; set; }

        public ICollection<UserWord> UserWords { get; set; }
        public ICollection<WordTranslation> Translations { get; set; }
    }
}
