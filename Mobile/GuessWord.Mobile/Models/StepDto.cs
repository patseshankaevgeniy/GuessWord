using System.Collections.Generic;

namespace GuessWord.Mobile.Models
{
    public class StepDto
    {
        public string Target { get; set; }
        public List<OptionDto> Options { get; set; }
    }
}