using GuessWord.BusinessLogic.Exceptions;
using GuessWord.BusinessLogic.Models;
using GuessWord.BusinessLogic.Services.Interfaces;
using GuessWord.BusinessLogic.Services.Mappers;
using GuessWord.DataAccess.Repositories;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Services
{
    public class WordsService : IWordsService
    {
        private readonly IWordsRepository _wordsRepository;
        private readonly IWordMapper _wordMapper;

        public WordsService(
            IWordsRepository wordsRepository,
            IWordMapper wordMapper
            )
        {
            _wordsRepository = wordsRepository;
            _wordMapper = wordMapper;
        }

        public async Task<List<WordDto>> GetAllAsync()
        {
            var words = await _wordsRepository.GetAllAsync();

           var wordsDto = words
                .Select(x => new WordDto
                {
                    Id = x.Id,
                    Language = x.Language,
                    Translations = x.Translations,
                    UserWords = x.UserWords,
                    Value = x.Value
                }).ToList();

            return wordsDto;
        }

        public async Task<WordDto> GetAsync(string value)
        {
            var word = await _wordsRepository.GetByNameAsync(value);
            if (word == null)
            {
                throw new NotFoundException($"Can't find word with id: {value}");
            }

            var wordDto = _wordMapper.Map(word);

            return wordDto;
        }

        public async Task<List<WordDto>> GetByLetterAsync(string letter)
        {
            var words = await _wordsRepository.GetByLetterAsync(letter);
            if (words == null)
            {
                throw new NotFoundException($"Can't find words with letter: {letter}");
            }

            return words
                .Select(x => new WordDto
                {
                    Id = x.Id,
                    Language = x.Language,
                    Translations = x.Translations,
                    UserWords = x.UserWords,
                    Value = x.Value
                })
                .ToList();
        }

        public async Task<WordDto> CreateAsync(string wordValue)
        {
            var word = new Word
            {
                Language = Language.English,
                Value = wordValue
            };

            word = await _wordsRepository.CreateAsync(word);
            var wordDto = _wordMapper.Map(word);

            return wordDto;
        }

        public Task<List<Word>> GetOptionsWordsAsync()
        {
            var optionWords = _wordsRepository.GetOptionsWordsAsync();
            return optionWords;
        }

        public async Task<List<WordWithTranslation>> GetWordsWithTranslation(int userId, WordStatus status)
        {
            var words =  _wordsRepository.GetWordsWithTranslation(userId, status);
            return words;
        }
    }
}
