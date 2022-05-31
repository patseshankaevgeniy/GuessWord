namespace GuessWord.Mobile.Application.Game.Models
{
    public class Option
    {
        public string Word { get; set; }
        public int OrderNumber { get; set; }
        public bool IsCorrect { get; set; }
    }
}