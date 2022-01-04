using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class BackendClient : IBackendClient
    {
        private const string BackendUrl = "https://eb8d-178-172-234-92.ngrok.io/api/";

        public async Task<TOut> PostAsync<TOut, TModel>(string url, TModel model)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException(nameof(url));
            }

            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var httpsClient = new HttpClient();
            var httpsResponse = await httpsClient.PostAsync(BackendUrl + url, data);
            if (httpsResponse.IsSuccessStatusCode)
            {
                var stringModel = await httpsResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TOut>(stringModel);
                return result;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
