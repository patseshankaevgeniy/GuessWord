namespace GuessWord.Mobile.Application.Common.Services
{
    public interface ICurrentUserService
    {
        string AccessToken { get; set; }
        bool IsSignedIn { get; set; }
    }
}