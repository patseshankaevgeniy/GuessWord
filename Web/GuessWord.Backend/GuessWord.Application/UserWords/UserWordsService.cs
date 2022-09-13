using AutoMapper;
using FluentValidation;
using GuessWord.Application.Common.Exceptions;
using GuessWord.Application.Common.Interfaces;
using GuessWord.Application.Common.Interfaces.Repositories;
using GuessWord.Application.Exceptions;
using GuessWord.Application.UserWords.Models;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidationException = GuessWord.Application.Exceptions.ValidationException;

namespace GuessWord.Application.UserWords
{
    public class UserWordsService : IUserWordsService
    {
        private readonly IGenericRepository<UserWord> _userWordsRepository;
        private readonly IGenericRepository<Word> _wordsRepository;

        private readonly ICurrentUserService _currentUser;
        private readonly IValidator<UserWordDto> _validator;
       // private readonly IUserWordMapper _mapper;
        private readonly IMapper _mapper;

        public UserWordsService(
            ICurrentUserService currentUser,
            IValidator<UserWordDto> validator,
            IMapper mapper,
            IGenericRepository<UserWord> userWordsRepository,
            IGenericRepository<Word> wordsRepository,
            IUserWordMapper wordMapper)
        {
            _currentUser = currentUser;
            _validator = validator;
            _userWordsRepository = userWordsRepository;
            _wordsRepository = wordsRepository;
            _mapper = mapper;
        }

        public async Task<List<UserWordDto>> GetAllAsync()
        {
            if (_currentUser.UserId < 1)
            {
                throw new UnauthorizedException("Please log in");
            }

            var userWords = await _userWordsRepository.FindAsync(
                x => x.UserId == _currentUser.UserId,
                x => x.Include(x => x.Word)
                      .ThenInclude(x => x.Translations)
                      .ThenInclude(x => x.Translation));

            return userWords.Select(_mapper.Map<UserWordDto>).ToList();
        }

        public async Task<UserWordDto> GetAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Id can't be less or equal zero");
            }

            var userWord = await _userWordsRepository.FirstAsync(
                x => x.Id == id,
                x => x.Include(x => x.Word)
                     .ThenInclude(x => x.Translations)
                     .ThenInclude(x => x.Translation));

            if (userWord == null)
            {
                throw new NotFoundException($"Can't find userWord with id: {id}");
            }

            return _mapper.Map<UserWordDto>(userWord);
        }

        public async Task<UserWordDto> CreateAsync(UserWordDto userWordDto)
        {
            var result = _validator.Validate(userWordDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.ToString());
            }

            var word = await _wordsRepository.FirstAsync(
                x => x.Value == userWordDto.Word,
                x => x.Include(x => x.Translations).ThenInclude(x => x.Translation));

            if (word == null)
            {
                if (userWordDto.Translations.Count == 0)
                {
                    throw new ValidationException("This word doesn't have translations!");
                }

                word = new Word
                {
                    Language = (Language)userWordDto.Language,
                    Value = userWordDto.Word,
                    Translations = userWordDto.Translations
                        .Select(translation => new WordTranslation
                        {
                            Translation = new Word { Value = translation }
                        })
                        .ToList()
                };
                word = await _wordsRepository.CreateAsync(word);
            }

            var userWord = new UserWord
            {
                WordId = word.Id,
                Word = word,
                UserId = _currentUser.UserId,
                Status = WordStatus.New,
                TargetRepeatNumber = 2
            };

            userWord = await _userWordsRepository.CreateAsync(userWord);
            userWordDto = _mapper.Map<UserWordDto>(userWord);
            return userWordDto;
        }

        public async Task UpdateAsync(int id, UserWordPatchDto userWordDto)
        {
            if (id <= 0)
            {
                throw new ValidationException("Id can't be less or equal zero");
            }

            if (!userWordDto.Status.HasValue)
            {
                throw new ValidationException("Status can't be null!");
            }

            var userWord = await _userWordsRepository.GetAsync(id);
            if (userWord == null)
            {
                throw new ValidationException($"Can't find word with {id}");
            }

            if (_currentUser.UserId != userWord.UserId)
            {
                throw new AccessViolationException("You have no rights");
            }

            userWord.Status = (WordStatus)userWordDto.Status;
            await _userWordsRepository.UpdateAsync(userWord);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Id is empty");
            }

            var userWord = await _userWordsRepository.GetAsync(id);
            if (userWord == null)
            {
                throw new NotFoundException($"Can't find userWord with id: {id}");
            }

            if (_currentUser.UserId != userWord.UserId)
            {
                throw new AccessViolationException("You have no rights");
            }

            await _userWordsRepository.DeleteAsync(userWord);
        }
    }
}
