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

        public async Task<UserWordDto> CreateUserWordAsync(UserWordDto userWordDto)
        {
            if (string.IsNullOrEmpty(userWordDto.Word))
            {
                throw new ValidationExeption("Word cannot be null or empty");
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
            userWordDto = _wordMapper.Mapp(userWord);

            return userWordDto;
        }

        public async Task<List<UserWordDto>> GetUserWordsAsync()
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
    }
}
