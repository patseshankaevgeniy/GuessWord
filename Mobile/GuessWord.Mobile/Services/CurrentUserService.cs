using Xamarin.Forms;

namespace GuessWord.Mobile.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string AccessToken
        {
            get => (string)Application.Current.Properties[nameof(AccessToken)];
            set => Application.Current.Properties[nameof(AccessToken)] = value;
        }
    }
}
