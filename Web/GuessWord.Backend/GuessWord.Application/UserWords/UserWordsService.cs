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

namespace GuessWord.Application.UserWords
{
    public class UserWordsService : IUserWordsService
    {
        private readonly IGenericRepository<UserWord> _userWordsRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IGenericRepository<Word> _wordsRepository;
        private readonly IUserWordMapper _mapper;

        public UserWordsService(
            ICurrentUserService currentUser,
            IGenericRepository<UserWord> userWordsRepository,
            IGenericRepository<Word> wordsRepository,
            IUserWordMapper wordMapper)
        {
            _currentUser = currentUser;
            _userWordsRepository = userWordsRepository;
            _wordsRepository = wordsRepository;
            _mapper = wordMapper;
        }

        public async Task<List<UserWordDto>> GetAllAsync()
        {
            var userWords = await _userWordsRepository.GetAllAsync();
            return userWords.Select(_mapper.Map).ToList();
        }

        public async Task<UserWordDto> GetAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Id can't be less or equal zero");
            }

            var userWord = await _userWordsRepository.GetAsync(id);
            if (userWord == null)
            {
                throw new NotFoundException($"Can't find userWord with id: {id}");
            }

            return _mapper.Map(userWord);
        }

        public async Task<UserWordDto> CreateAsync(UserWordDto userWordDto)
        {
            var word = await _wordsRepository.FirstAsync(
                x => x.Value == userWordDto.Word,
                x => x.Include(x => x.Translations).ThenInclude(x => x.Translation));

            if (word == null)
            {
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
            return _mapper.Map(userWord);
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
