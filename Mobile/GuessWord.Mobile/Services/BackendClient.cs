using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class BackendClient : IBackendClient
    {
        private const string BackendUrl = "https://4941-178-172-234-92.ngrok.io/api/";
        private readonly ICurrentUserService _userService;

        public BackendClient(ICurrentUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<TOut>> GetAllAsync<TOut>(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException(nameof(url));
            }

            using var httpsClient = new HttpClient();
            httpsClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userService.AccessToken);
            var httpsResponse = await httpsClient.GetAsync(BackendUrl + url);
            if (httpsResponse.IsSuccessStatusCode)
            {
                var stringModel = await httpsResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<TOut>>(stringModel);
                return result;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<TOut> GetAsync<TOut>(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException(nameof(url));
            }

            using var httpsClient = new HttpClient();
            httpsClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userService.AccessToken);
            var httpsResponse = await httpsClient.GetAsync(BackendUrl + url);
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

        public async Task<TOut> DeleteAsync<TOut>(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException(nameof(url));
            }

            using var httpsClient = new HttpClient();
            httpsClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userService.AccessToken);
            var httpsResponse = await httpsClient.GetAsync(BackendUrl + url);
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
