using GuessWord.Mobile.Clients;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class WordService : IWordService
    {
        private readonly IGuessWordApiClient _apiClient;

        public WordService(IGuessWordApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<List<Models.Word>> GetAllAsync()
        {
            var words = await _apiClient.GetWordsAsync();

            return words.Select(x => new Models.Word
            {
                Id = x.Id,
                Value = x.Value,
                Translations = x.Translations.Aggregate((x, y) =>
                                                         x.Length == 1 ?
                                                         x + y : x + ", " + y),
            })
                .ToList();
        }

        public async Task<List<Models.Word>> GetByLetterAsync(string letter)
        {
            var words = await _apiClient.GetByLetterAsync(letter);

            if (words != null)
            {
                return words
                    .Select(x => new Models.Word
                    {
                        Id = x.Id,
                        Value = x.Value,
                        Translations = x.Translations.Aggregate((aggregate, value) =>
                                                                aggregate.Length == 1 ?
                                                                aggregate + value : aggregate + ", " + value)
                    })
                    .ToList();
            }
            return null;
        }

        public async Task<Models.Word> GetAsync(string value)
        {
            var wordDto = await _apiClient.GetWordAsync(value);

            var word = new Models.Word
            {
                Id = wordDto.Id,
                Value = wordDto.Value,
                Translations = wordDto.Translations.Aggregate((aggregate, value) =>
                                                                 aggregate.Length == 1 ?
                                                                 aggregate + value : aggregate + ", " + value)
            };

            return word;
        }

        public async Task<Models.Word> CreateAsync(string value)
        {
            var wordDto = await _apiClient.CreateWordAsync(value);

            var word = new Models.Word
            {
                Id = wordDto.Id,
                Value = wordDto.Value,
                Translations = wordDto.Translations.Aggregate((aggregate, value) =>
                                                                 aggregate.Length == 1 ?
                                                                 aggregate + value : aggregate + ", " + value)
            };

            return word;
        }


        public async Task DeleteAsync(int id)
        {
            // await _apiClient.(id);
        }

    }
}
