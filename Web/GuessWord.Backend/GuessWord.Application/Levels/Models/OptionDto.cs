namespace GuessWord.Application.Levels.Models
{
    public class OptionDto
    {
        public string Word { get; set; }
        public int OrderNumber { get; set; }
        public bool IsCorrect { get; set; }
    }
}