namespace GuessWord.Api.Models
{
    public class SignInResult
    {
        public bool Succeeded { get; set; }
        public AuthErrorType? ErrorType { get; set; }
    }
}
