using GuessWord.Mobile.Clients;
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

        public async Task<List<Models.UserWord>> GetAllAsync()
        {
            var userWords = await _apiClient.GetUserWordsAsync();

            if (userWords != null)
            {
                return userWords
                    .Select(x => new Models.UserWord
                    {
                        Id = x.Id,
                        Word = x.Word,
                        Status = x.Status.ToString(),
                        Translations = x.Translations.Aggregate((aggregate, value) =>
                                                                 aggregate.Length == 1 ?
                                                                 aggregate + value : aggregate + ", " + value)
                    })
                    .ToList();
            }
            return null;
        }

        public async Task<Models.UserWord> GetAsync(int id)
        {
            var userWordDto = await _apiClient.GetUserWordAsync(id);

            var userWord = new Models.UserWord
            {
                Id = userWordDto.Id,
                Word = userWordDto.Word,
                Status = userWordDto.Status.ToString(),
            };

            if (userWordDto.Translations.Count != 0)
            {
                userWord.Translations = userWordDto.Translations.Aggregate((left, right) => left + " , " + right);
            }

            return userWord;
        }

        public async Task<Models.UserWord> CreateAsync(string word)
        {
            var userWordDto = await _apiClient.CreateUserWordAsync(word);

            var newUserWord = new Models.UserWord
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

        public async Task<Models.UserWord> UpdateAsync(string status, int id)
        {
            int? wordStatus = new int?();
            if (status == "Done")
            {
                wordStatus = 2;
            }
            if (status == "In progress")
            {
                wordStatus = 1;
            }
            if (status == "New")
            {
                wordStatus = 0;
            }

            var userWordDto = await _apiClient.UpdateUserWordAsync(id, wordStatus);

            var userWord = new Models.UserWord
            {
                Id = userWordDto.Id,
                Word = userWordDto.Word,
                Status = userWordDto.Status.ToString(),
            };

            if (userWordDto.Translations.Count != 0)
            {
                userWord.Translations = userWordDto.Translations.Aggregate((left, right) => left + " , " + right);
            }

            return userWord;

        }

        public async Task DeleteAsync(int id)
        {
            await _apiClient.DeleteUserWordAsync(id);
        }
    }
}
