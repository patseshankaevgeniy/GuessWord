namespace GuessWord.BusinessLogic.Models
{
    public class SignInResultDto
    {
        public bool Succeeded { get; set; }
        public int? ErrorType { get; set; }
        public string Token { get; set; }
    }
}
