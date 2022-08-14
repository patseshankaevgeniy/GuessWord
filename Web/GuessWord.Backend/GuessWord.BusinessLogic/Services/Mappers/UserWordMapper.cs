using GuessWord.BusinessLogic.Models;
using GuessWord.Domain.Entities;
using System.Linq;

namespace GuessWord.BusinessLogic.Services.Mappers
{
    public class UserWordMapper : IUserWordMapper
    {
        public UserWordDto Map(UserWord userWord)
        {
            var userWordDto = new UserWordDto
            {
                Id = userWord.Id,
                Word = userWord.Word.Value,
                Language = (int)userWord.Word.Language,
                Status = (int)userWord.Status,
                Translations = userWord.Word.Translations
                         .Select(x => x.Translation.Value)
                         .ToList()
            };

            return userWordDto;
        }
    }
}
