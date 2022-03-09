using System.Collections.Generic;

namespace GuessWord.Api.Models
{
    public class LevelDto
    {
        public List<StepDto> Steps { get; set; }
        public int Count { get; set; }
    }
}
