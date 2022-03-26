using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.DataAccess.Repositories
{
    public class WordsRepository : IWordsRepository
    {
        private readonly ApplicationDbContext _db;

        public WordsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Word> CreateAsync(Word newWord)
        {
            _db.Words.Add(newWord);
            await _db.SaveChangesAsync();
            return newWord;
        }

        public async Task<List<Word>> GetOptionsWordsAsync()
        {
            var words = await _db.Words
                .Where(x => x.Language == Language.Russian)
                .ToListAsync();

            return words;
        }

        public async Task<Word> GetByNameAsync(string value)
        {
            var word = await _db.Words
                .Include(x => x.Translations)
                .ThenInclude(x => x.Translation)
                .FirstOrDefaultAsync(x => x.Value == value);
            return word;
        }

        public List<WordWithTranslation> GetWordsWithTranslation(int userId, WordStatus status)
        {
            var result = from userWord in _db.UsersWords
                         join word in _db.Words on userWord.WordId equals word.Id
                         join translation in _db.Translations on word.Id equals translation.WordId
                         join word1 in _db.Words on translation.TranslationId equals word1.Id
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
