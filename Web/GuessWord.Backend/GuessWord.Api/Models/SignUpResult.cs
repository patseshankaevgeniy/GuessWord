namespace GuessWord.Api.Models
{
    public class SignUpResult
    {
        public bool Succeeded { get; set; }
        public AuthErrorType? ErrorType { get; set; }
    }
}
