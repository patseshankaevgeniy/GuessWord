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
        private readonly IGenericRepository<UserWord> userWordsRepository;
        private readonly IGenericRepository<Word> WordsRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserWordsRepository _userWordsRepository;
        private readonly IWordsRepository _wordsRepository;
        private readonly IUserWordMapper _wordMapper;

        public UserWordsService(
            ICurrentUserService currentUser,
            IGenericRepository<UserWord> UserWordsRepository,
            IGenericRepository<Word> WordsRepository,
            IUserWordsRepository userWordsRepository,
            IWordsRepository wordsRepository,
            IUserWordMapper wordMapper)
        {
            this.WordsRepository = WordsRepository;
            this.userWordsRepository = UserWordsRepository;
            _currentUser = currentUser;
            _userWordsRepository = userWordsRepository;
            _wordsRepository = wordsRepository;
            _wordMapper = wordMapper;
        }

        public async Task<List<UserWordDto>> GetAllAsync()
        {
            var userWords = await userWordsRepository.FindAsync(x => x.UserId == _currentUser.UserId);
            var userWordDtos = userWords
                .Select(userWord => new UserWordDto
                {
                    Id = userWord.Id,
                    Word = userWord.Word.Value,
                    Status = (int)userWord.Status,
                    Translations = userWord.Word.Translations
                        .Select(x => x.Translation.Value)
                        .ToList()
                })
                .ToList();

            return userWordDtos;
        }

        public async Task<UserWordDto> GetAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Wrong id");
            }

            var userWord = await userWordsRepository.GetAsync(id);
            if (userWord == null)
            {
                throw new NotFoundException($"Can't find userWord with id: {id}");
            }

            var userWordDto = _wordMapper.Map(userWord);

            return userWordDto;
        }

        public async Task<UserWordDto> CreateAsync(UserWordDto newUserWordDto)
        {
            var word = (await WordsRepository.FindAsync(
                x => x.Value.Contains(newUserWordDto.Word),
                x => x.Include(x => x.Translations).ThenInclude(x => x.Translation)))
                      .FirstOrDefault();
            if (word == null)
            {
                var newWord = new Word
                {
                    Language = (Language)newUserWordDto.Language,
                    Value = newUserWordDto.Word
                };
                word = await _wordsRepository.CreateAsync(newWord);
            }

            var newUserWord = new UserWord
            {
                Status = WordStatus.New,
                Word = word,
                UserId = _currentUser.UserId,
                TargetRepeatNumber = 2
            };

            var userWord = await _userWordsRepository.CreateAsync(newUserWord);
            var userWordDto = _wordMapper.Map(userWord);

            return userWordDto;
        }

        public async Task UpdateAsync(int id, UserWordPatchDto userWordDto)
        {
            if (id <= 0)
            {
                throw new ValidationException("");
            }

            if (!userWordDto.Status.HasValue)
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

            userWord.Status = (WordStatus)userWordDto.Status;
            userWord = await _userWordsRepository.UpdateAsync(userWord);
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
                throw new AccessViolationException("No rights");
            }

            await _userWordsRepository.RemoveAsync(userWord);
        }
    }
}
