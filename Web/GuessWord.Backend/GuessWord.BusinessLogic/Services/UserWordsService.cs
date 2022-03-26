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
    public class UserWordsService : IUserWordsService
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUserWordsRepository _userWordsRepository;
        private readonly IWordsRepository _wordsRepository;
        private readonly IUserWordMapper _wordMapper;

        public UserWordsService(
            ICurrentUserService currentUser,
            IUserWordsRepository userWordsRepository,
            IWordsRepository wordsRepository,
            IUserWordMapper wordMapper)
        {
            _currentUser = currentUser;
            _userWordsRepository = userWordsRepository;
            _wordsRepository = wordsRepository;
            _wordMapper = wordMapper;
        }

        public async Task<List<UserWordDto>> GetAllAsync()
        {
            var userWords = await _userWordsRepository.GetAllAsync(_currentUser.UserId);
            var userWordDtos = userWords
                .Select(userWord => new UserWordDto
                {
                    Id = userWord.Id,
                    Word = userWord.Word.Value,
                    Translations = userWord.Word.Translations
                        .Select(x => x.Translation.Value)
                        .ToList()
                })
                .ToList();

            return userWordDtos;
        }

        public async Task<UserWordDto> GetAsync(int id)
        {
            var userWord = await _userWordsRepository.GetAsync(id);
            if (userWord == null)
            {
                throw new NotFoundException($"Can't find userWord with id: {id}");
            }

            var userWordDto = _wordMapper.Map(userWord);

            return userWordDto;
        }

        public async Task<UserWordDto> CreateAsync(UserWordDto userWordDto)
        {
            if (string.IsNullOrEmpty(userWordDto.Word))
            {
                throw new ValidationException("Word cannot be null or empty");
            }

            var word = await _wordsRepository.GetByNameAsync(userWordDto.Word);
            if (word == null)
            {
                word = new Word
                {
                    Language = Language.English,
                    Value = userWordDto.Word
                };
                word = await _wordsRepository.CreateAsync(word);
            }

            var userWord = new UserWord
            {
                Status = userWordDto.Status,
                Word = word,
                UserId = _currentUser.UserId,
                TargetRepeatNumber = 2
            };

            userWord = await _userWordsRepository.CreateAsync(userWord);
            userWordDto = _wordMapper.Map(userWord);

            return userWordDto;
        }

        public async Task<UserWordDto> UpdateAsync(UserWordDto userWordDto, int id)
        {
            if (userWordDto == null)
            {
                throw new ValidationException("bad word");
            }

            var userWord = await _userWordsRepository.GetAsync(id);

            if (userWord == null)
            {
                throw new ValidationException("No word");
            }

            if (_currentUser.UserId != userWord.UserId)
            {
                throw new AccessViolationException("No rights");
            }

            userWord.Status = userWordDto.Status;

            userWord = await _userWordsRepository.UpdateAsync(userWord);

            userWordDto = _wordMapper.Map(userWord);
            return userWordDto;

        }

        public async Task DeleteAsync(int id)
        {
            var userWord = await _userWordsRepository.GetAsync(id);
            if (userWord == null)
            {
                throw new NotFoundException($"Can't find userWord with id: {id}");
            }

            if (_currentUser.UserId != userWord.UserId)
            {
                throw new AccessViolationException("No rights");
            }

            await _userWordsRepository.RemoveAsync(userWord);
        }
    }
}
