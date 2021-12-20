using GuessWord.Mobile.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace GuessWord.Mobile.Services
{
    public class AuthService : IAuthService
    {
 
        private const string SignInUrl = "https://403b-178-172-234-92.ngrok.io/api/auth/signIn";

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
            throw new System.NotImplementedException();
        }
    }
}
