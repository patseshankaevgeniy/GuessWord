namespace GuessWord.BusinessLogic.Models
{
    public class AddWordResult
    {
        public bool Succeeded { get; set; }
        public WordErrorType wordErrorType { get; set; }
    }
}
