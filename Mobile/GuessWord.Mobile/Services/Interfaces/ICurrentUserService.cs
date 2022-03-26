namespace GuessWord.Mobile.Services
{
    public interface ICurrentUserService
    {
        string AccessToken { get; set; }
        bool IsSignedIn { get; set; }
    }
}