using Xamarin.Forms;

namespace GuessWord.Mobile.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string AccessToken
        {
            get
            {
                var properties = Application.Current.Properties;
                return properties.ContainsKey(nameof(AccessToken))
                     ? (string)Application.Current.Properties[nameof(AccessToken)]
                     : "";
            }
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

    public class FakeCurrentUserService : ICurrentUserService
    {
        public string AccessToken
        {
            get => "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRmFudGEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiYWRtaW4iLCJ1c2VyIl0sImV4cCI6MTY0ODU2NTk0NywiaXNzIjoiVGVzdC5jb20iLCJhdWQiOiJUZXN0LmNvbSJ9.N4d93zfLMdUvDYVkvFebSG7HY0bu8Rplj1g0pU6J58E";
            set { }
        }

        public bool IsSignedIn
        {
            get => true;
            set { }
        }
    }
}
