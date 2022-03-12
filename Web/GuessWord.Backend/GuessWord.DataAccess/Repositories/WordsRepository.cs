using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GuessWord.DataAccess.Repositories
{
    public class WordsRepository : IWordsRepository
    {
        private readonly ApplicationDbContext _db;

        public WordsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Word> GetOptionsWords(int userId)
        {
            var result = from userWord in _db.UsersWords
                         join words in _db.Words on userWord.Id equals words.Id
                         join tranlation in _db.Translations on userWord.Id equals tranlation.Id
                         where userWord.Id != userId && userWord.WordId != userId
                         select new Word
                         {
                             Id = words.Id,
                             Value = words.Value,
                             Language = words.Language
                         };
            return result.ToList();
        }

        public List<WordWithTranslation> GetWordsWithTranslation(int userId, WordStatus status)
        {
            var result = from userWord in _db.UsersWords
                         join word in _db.Words on userWord.WordId equals word.Id
                         join translation in _db.Translations on word.Id equals translation.WordId
                         join word1 in _db.Words on translation.WordTranslationId equals word1.Id
                         where userWord.UserId == userId && userWord.Status == status
                         select new WordWithTranslation
                         {
                             Value = word.Value,
                             Translation = word1.Value
                         };

            return result.ToList();
        }
    }
}
