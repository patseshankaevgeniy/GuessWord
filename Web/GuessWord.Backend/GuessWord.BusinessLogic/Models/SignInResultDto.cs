namespace GuessWord.BusinessLogic.Models
{
    public class SignInResultDto
    {
        public bool Succeeded { get; set; }
        public AuthErrorType? ErrorType { get; set; }
    }
}
