using GuessWord.Mobile.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private const string SignInUrl = "https://ab89-178-172-234-92.ngrok.io/api/auth/signIn";
        private const string SignUpUrl = "https://c378-178-172-234-92.ngrok.io/api/auth/signUp";

        public  SignInResult TrySignIn(string login, string password)
        {
            var signInModel = new { UserName = login, Password = password };
            var json = JsonConvert.SerializeObject(signInModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            var httpResponse = httpClient.PostAsync(SignInUrl, data).Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var stringModel = httpResponse.Content.ReadAsStringAsync().Result;
                var signInResult = JsonConvert.DeserializeObject<SignInResult>(stringModel);
                return signInResult;
            }
            else
            {
                var result = new SignInResult { Succeeded = false, ErrorType = AuthErrorType.UnknowExeption };
                return result;
            }
        }

        public SignUpResult TrySignUp(string login, string password, string username)
        {
            var signUpModel = new { UserLogin = login, UserPassword = password, UserName = username };
            var json = JsonConvert.SerializeObject(signUpModel);
            var data = new StringContent(json,Encoding.UTF8, "application/json");

            var httpsClient = new HttpClient();
            var httpsResponse = httpsClient.PostAsync(SignUpUrl, data).Result;
            if (httpsResponse.IsSuccessStatusCode)
            {
                var stringModel = httpsResponse.Content.ReadAsStringAsync().Result;
                var signUpResult = JsonConvert.DeserializeObject<SignUpResult>(stringModel);
                return signUpResult;
            }
            else
            {
            var result = new SignUpResult {Success = true, ErrorType = AuthErrorType.UnknowExeption};
            return result; 
            }
        }

        public Task<bool> CheckSignInAsync()
        {
            return Task.FromResult(false);
        }
    }
}
