using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public interface IBackendClient
    {
        Task<TOut> GetAsync<TOut>(string url);
        Task<TOut> PostAsync<TOut, TModel>(string url, TModel model);
    }
}