using GuessWord.Mobile.Clients;
using GuessWord.Mobile.Models;
using GuessWord.Mobile.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class UserWordService : IUserWordService
    {
        private readonly IGuessWordApiClient _apiClient;

        public UserWordService(IGuessWordApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<UserWord>> GetAllAsync()
        {
            var userWords = await _apiClient.GetUserWordsAsync();


            return userWords
                .Select(x => new UserWord
                {
                    Id = x.Id,
                    Word = x.Word,
                    Status = x.Status.ToString(),
                    Translations = x.Translations.Aggregate((left, right) => left + " , " + right)
                })
                .ToList();
        }

        public async Task<UserWord> GetAsync(int id)
        {
            var userWordDto = await _apiClient.GetUserWordByIdAsync(id);

            var userWord = new UserWord
            {
                Id = userWordDto.Id,
                Word = userWordDto.Word,
                Status = userWordDto.Status.ToString(),
                Translations = userWordDto.Translations.Aggregate((left, right) => left + " , " + right)
            };
            
            return userWord;
        }

        public async Task<UserWord> CreateAsync(string word)
        {
            
           var userWordDto = await _apiClient.CreateUserWordAsync(word);

            var newUserWord = new UserWord 
            {
                Id = userWordDto.Id,
                Word = userWordDto.Word,
                Status = userWordDto.Status.ToString()
            };

            if (userWordDto.Translations.Count != 0)
            {
                newUserWord.Translations = userWordDto.Translations.Aggregate((left, right) => left + " , " + right);
            }

            return newUserWord;

        }

        public Task<UserWord> UpdateAsync(UserWord userWord, int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            //var result = await _backendClient.DeleteAsync<ActionResult>($"user-words/{id}");
            //return Task.FromResult(result);
            throw new System.Exception();
        }
    }
}
