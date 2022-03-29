using GuessWord.Mobile.Clients;
using GuessWord.Mobile.Models;
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
            //var userWordDto = await _backendClient.GetAsync<UserWordDto>($"user-words/{id}");

            //var userWord = new UserWord
            //{
            //    Id = userWordDto.Id,
            //    Word = userWordDto.Word,
            //    Status = userWordDto.Status.ToString(),
            //    Translations = userWordDto.Translations.Aggregate((left, right) => left + " , " + right)
            //};
            var userWord = new UserWord();
            return userWord;
        }

        public async Task<UserWord> CreateAsync(UserWord userWord)
        {
            //var userWordDto = new UserWordDto
            //{
            //    Word = userWord.Word,
            //    Status = Models.Enums.WordStatus.New,
            //    Translations = userWord.Translations.Split(" , ").ToList()
            //};
            //userWordDto = await _backendClient.PostAsync<UserWordDto,UserWordDto>("user-words", userWordDto);

            //var newUserWord = new UserWord
            //{
            //    Id = userWordDto.Id,
            //    Word = userWordDto.Word,
            //    Status = userWordDto.Status.ToString(),
            //    Translations = userWordDto.Translations.Aggregate((left, right) => left + " , " + right)
            //};

            return userWord;

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
