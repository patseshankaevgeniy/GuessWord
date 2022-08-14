using System.Collections.Generic;

namespace GuessWord.Application.Words.Models
{
    public class WordDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int Language { get; set; }
        public List<string> Translations { get; set; }
    }
}
