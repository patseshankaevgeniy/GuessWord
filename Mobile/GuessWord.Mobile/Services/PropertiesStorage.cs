using GuessWord.Mobile.Application.Common.Interfaces;

namespace GuessWord.Mobile.Services
{
    public class PropertiesStorage : IPropertiesStorage
    {
        public T Get<T>(string key)
        {
            if (App.Current.Properties.TryGetValue(key, out var value))
            {
                return (T)value;
            }
            return default(T);
        }

        public void Set<T>(string key, T value)
        {
            App.Current.Properties[key] = value;
        }
    }
}
