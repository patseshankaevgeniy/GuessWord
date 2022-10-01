using AutoMapper;
using FluentValidation;
using GuessWord.Application.Common.Interfaces.Repositories;
using GuessWord.Application.Words.Models;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidationException = GuessWord.Application.Exceptions.ValidationException;

namespace GuessWord.Application.Words
{
    public class WordsService : IWordsService
    {
        private readonly IGenericRepository<Word> _wordsRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<WordDto> _validator;

        public WordsService(
            IGenericRepository<Word> WordsRepository,
            IMapper wordMapper,
            IValidator<WordDto> validator)
        {
            _wordsRepository = WordsRepository;
            _mapper = wordMapper;
            _validator = validator;
        }

        public async Task<List<WordDto>> FindAsync(string letter = null)
        {
            var words = string.IsNullOrEmpty(letter)
                ? await _wordsRepository.FindAsync(
                    x => x.Language == Language.English,
                    x => x.Include(x => x.Translations).ThenInclude(x => x.Translation))
                : await _wordsRepository.FindAsync(
                    x => x.Value.Contains(letter),
                    x => x.Include(x => x.Translations).ThenInclude(x => x.Translation));

            return words.Select(_mapper.Map<WordDto>).ToList();
        }

        public async Task<WordDto> CreateAsync(WordDto wordDto)
        {
            var result = _validator.Validate(wordDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.ToString());
            }

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
            return _mapper.Map<WordDto>(word);
        }
    }
}
