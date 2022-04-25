using System.Collections.Generic;

namespace GuessWord.BusinessLogic.Models
{
    public class WordDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<string> Translations { get; set; }
    }
}
