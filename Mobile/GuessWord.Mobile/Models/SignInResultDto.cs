namespace GuessWord.Mobile.Models
{
    public class SignInResultDto
    {
        public bool Succeeded { get; set; }
        public AuthErrorType? ErrorType { get; set; }
        public string Token { get; set; }
    }
}
