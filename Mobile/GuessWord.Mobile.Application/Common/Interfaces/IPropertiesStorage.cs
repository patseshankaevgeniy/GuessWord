namespace GuessWord.Mobile.Application.Common.Interfaces
{
    public interface IPropertiesStorage
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}
