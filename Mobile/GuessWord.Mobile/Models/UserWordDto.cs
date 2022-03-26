using GuessWord.Mobile.Models.Enums;
using System.Collections.Generic;

namespace GuessWord.Mobile.Models
{
    public class UserWordDto
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public WordStatus Status { get; set; }
        public List<string> Translations { get; set; }
    }
}
