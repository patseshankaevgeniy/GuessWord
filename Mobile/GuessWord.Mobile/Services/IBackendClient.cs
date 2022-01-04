using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public interface IBackendClient
    {
        Task<TOut> PostAsync<TOut, TModel>(string url, TModel model);
    }
}