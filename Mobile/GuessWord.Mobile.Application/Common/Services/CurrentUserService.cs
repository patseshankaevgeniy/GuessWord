using GuessWord.Mobile.Application.Common.Interfaces;

namespace GuessWord.Mobile.Application.Common.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IPropertiesStorage _propertiesStorage;

        public CurrentUserService(IPropertiesStorage propertiesStorage)
        {
            _propertiesStorage = propertiesStorage;
        }

        public string AccessToken
        {
            get => _propertiesStorage.Get<string>(nameof(AccessToken));
            set => _propertiesStorage.Set(nameof(AccessToken), value);
        }

        public bool IsSignedIn
        {
            get => _propertiesStorage.Get<bool>(nameof(IsSignedIn));
            set => _propertiesStorage.Set(nameof(IsSignedIn), value);
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
