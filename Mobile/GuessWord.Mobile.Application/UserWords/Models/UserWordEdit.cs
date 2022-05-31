using System.Collections.Generic;

namespace GuessWord.Mobile.Application.UserWords.Models
{
    public class UserWordEdit
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Status { get; set; }
        public ICollection<Translation> Translations { get; set; }
    }
}
