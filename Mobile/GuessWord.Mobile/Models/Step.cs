using System.Collections.Generic;

namespace GuessWord.Mobile.Models
{
    public class Step
    {
        public string Target { get; set; }
        public List<Option> Options { get; set; }
    }
}