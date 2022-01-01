namespace GuessWord.BusinessLogic.Models
{
    public class SignUpResultDto
    {
        public bool Succeeded { get; set; }
        public AuthErrorType? ErrorType { get; set; }
    }
}
