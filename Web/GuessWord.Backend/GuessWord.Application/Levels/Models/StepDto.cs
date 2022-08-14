using System.Collections.Generic;

namespace GuessWord.Application.Levels.Models
{
    public class StepDto
    {
        public string Target { get; set; }
        public List<OptionDto> Options { get; set; }
    }
}