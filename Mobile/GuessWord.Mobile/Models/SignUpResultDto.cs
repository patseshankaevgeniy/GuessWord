namespace GuessWord.Mobile.Models
{
    public class SignUpResultDto
    {
        public bool Succeeded { get; set; }
        public AuthErrorType? ErrorType { get; set; }

    }
}
