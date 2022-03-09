using System.Collections.Generic;

namespace GuessWord.Api.Models
{
    public class StepDto
    {
        public string Target { get; set; }
        public List<OptionDto> Options { get; set; }
    }
}