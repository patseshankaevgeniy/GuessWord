using System.Collections.Generic;

namespace GuessWord.Mobile.Models
{
    public class LevelDto
    {
        public List<StepDto> Steps { get; set; }
        public int Count { get; set; }
    }
}
