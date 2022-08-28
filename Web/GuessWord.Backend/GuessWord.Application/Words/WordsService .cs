using GuessWord.Application.Common.Interfaces.Repositories;
using GuessWord.Application.Words.Models;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Application.Words
{
    public class WordsService : IWordsService
    {
        private readonly IGenericRepository<Word> _wordsRepository;
        private readonly IWordMapper _wordMapper;

        public WordsService(
            IGenericRepository<Word> WordsRepository,
            IWordMapper wordMapper)
        {
            _wordsRepository = WordsRepository;
            _wordMapper = wordMapper;
        }

        public async Task<List<WordDto>> FindAsync(string letter = null)
        {
            var words = string.IsNullOrEmpty(letter)
                ? await _wordsRepository.GetAllAsync()
                : await _wordsRepository.FindAsync(
                    x => x.Value.Contains(letter),
                    x => x.Include(x => x.Translations).ThenInclude(x => x.Translation));

            return words.Select(_wordMapper.Map).ToList();
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
            return _wordMapper.Map(word);
        }
    }
}
