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

        public bool IsSignedIn
        {
            get
            {
                var properties = Application.Current.Properties;
                return properties.ContainsKey(nameof(IsSignedIn)) && (bool)properties[nameof(IsSignedIn)];
            }
            set => Application.Current.Properties[nameof(IsSignedIn)] = value;
        }
    }
}
