using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public interface IBackendClient
    {
        Task<TOut> GetAsync<TOut>(string url);
        Task<List<TOut>> GetAllAsync<TOut>(string url);
        Task<TOut> PostAsync<TOut, TModel>(string url, TModel model);
        Task<TOut> DeleteAsync<TOut>(string url);
    }
}