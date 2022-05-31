using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.UserWords.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.UserWords.Services
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

            if (userWords != null)
            {
                return userWords
                    .Select(x => new UserWord
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

        public async Task<UserWord> GetAsync(int id)
        {
            var userWordDto = await _apiClient.GetUserWordAsync(id);

            var userWord = new UserWord
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

        public async Task<UserWord> UpdateAsync(UserWord upUserWord)
        {
            int? wordStatus = new int?();
            if (upUserWord.Status == "Done")
            {
                wordStatus = 2;
            }
            if (upUserWord.Status == "In progress")
            {
                wordStatus = 1;
            }
            if (upUserWord.Status == "New")
            {
                wordStatus = 0;
            }

            var userWordDto = await _apiClient.UpdateUserWordAsync(upUserWord.Id, wordStatus);

            var userWord = new UserWord
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

        public Task<UserWordEdit> GetForEdditAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
