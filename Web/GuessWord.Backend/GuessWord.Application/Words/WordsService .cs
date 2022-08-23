using GuessWord.Application.Common.Interfaces.Repositories;
using GuessWord.Application.Exceptions;
using GuessWord.Application.Words.Models;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Application.Words
{
    public class WordsService : IWordsService
    {
        //private readonly IWordsRepository _wordsRepository;
        //private readonly IGenericRepository<Word> _wordsRepository;
        //private readonly IGenericRepository<Word> _translationRepository;
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

        public async Task<List<WordDto>> FindAsync(string letter, string word)
        {
            if (!string.IsNullOrEmpty(letter))
            {
                return await GetByLetterAsync(letter);
            }

            var words = await _wordsRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(word))
            {
                return await GetAsync(word);
            }

            //var words = await _wordsRepository.GetAllAsync();
            var wordDtos = words.Select(x => _wordMapper.Map(x)).ToList();

            return wordDtos;
        }

        public async Task<List<WordDto>> GetAsync(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ValidationException("Wrong id");
            }

            var words = await _wordsRepository.GetByNameAsync(value);
            var wordDtos = words.Select(x => _wordMapper.Map(x)).ToList();

            return wordDtos;
        }

        public async Task<List<WordDto>> GetByLetterAsync(string letter)
        {
            if (string.IsNullOrEmpty(letter))
            {
                throw new ValidationException("Wrong id");
            }

            //var words = await _wordsRepository.FindAsync(
            //    x => x.Value.Contains(letter),
            //    x => x.Include(x=> x.Translations).ThenInclude(x=> x.Translation));
            var words = await _wordsRepository.GetByLetterAsync(letter);
            if (words == null)
            {
                throw new NotFoundException($"Can't find words with letter: {letter}");
            }

            return words
                .Select(x => new WordDto
                {
                    Id = x.Id,
                    Translations = x.Translations
                        .Select(x => x.Translation.Value)
                        .ToList(),
                    Value = x.Value
                })
                .ToList();
        }

        public async Task<WordDto> CreateAsync(WordDto wordDto)
        {
            var word = new Word
            {
                Language = Language.English,
                Value = wordDto.Value,
                Translations = wordDto.Translations
                                .Select(value => new WordTranslation
                                {
                                   Translation = new Word 
                                   { 
                                       Value = value,
                                       Language = Language.Russian
                                   } 
                                })
                                .ToList()
            };

            word = await _wordsRepository.CreateAsync(word);
            wordDto = _wordMapper.Map(word);

            return wordDto;
        }

        public Task<List<Word>> GetOptionsWordsAsync()
        {
            //var optionWords = _wordsRepository.FindAsync(x => x.Language == Language.Russian);
            return null;
        }

        public async Task<List<WordWithTranslation>> GetWordsWithTranslation(int userId, WordStatus status)
        {
            var words = _wordsRepository.GetWordsWithTranslation(userId, status);
            return words;
        }
    }
}
