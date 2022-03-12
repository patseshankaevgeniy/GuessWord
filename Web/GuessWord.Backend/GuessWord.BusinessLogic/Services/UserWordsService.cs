using GuessWord.BusinessLogic.Models;
using GuessWord.BusinessLogic.Services.Interfaces;
using GuessWord.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Services
{
    public class UserWordsService : IUserWordsService
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUserWordsRepository _userWordsRepository;

        public UserWordsService(
            ICurrentUserService currentUser,
            IUserWordsRepository userWordsRepository)
        {
            _currentUser = currentUser;
            _userWordsRepository = userWordsRepository;
        }

        public async Task<List<UserWordDto>> GetUserWordsAsync()
        {
            var userWords = await _userWordsRepository.GetUserWordsAsync(_currentUser.UserId);
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
