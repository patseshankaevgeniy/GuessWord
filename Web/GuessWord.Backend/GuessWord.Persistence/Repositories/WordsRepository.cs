using GuessWord.Application.Common.Interfaces.Repositories;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Persistence.Repositories
{
    public class WordsRepository : IWordsRepository
    {
        private readonly ApplicationDbContext _db;

        public WordsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Word>> GetAllAsync()
        {
            var words = await _db.Translations
                .Include(x => x.Word)
                .ThenInclude(x => x.Translations)
                .ThenInclude(x => x.Translation)
                .Select(x => new Word
                {
                    Id = x.WordId,
                    Language = x.Word.Language,
                    Translations = x.Word.Translations,
                    Value = x.Word.Value
                }).ToListAsync();



            return words;
        }

        public async Task<List<Word>> GetByNameAsync(string value)
        {
            var words = await _db.Translations
                 .Include(x => x.Word)
                 .ThenInclude(x => x.Translations)
                 .ThenInclude(x => x.Translation)
                 .Where(x => x.Word.Value == value)
                 .Select(x => new Word
                 {
                     Id = x.WordId,
                     Language = x.Word.Language,
                     Translations = x.Word.Translations,
                     Value = x.Word.Value
                 }).ToListAsync();

            return words;
        }

        public async Task<List<Word>> GetByLetterAsync(string letter)
        {
            var words = await _db.Translations
                .Include(x => x.Word)
                .ThenInclude(x => x.Translations)
                .ThenInclude(x => x.Translation)
                .Where(x => x.Word.Value.StartsWith(letter))
                .Select(x => new Word
                {
                    Id = x.WordId,
                    Language = x.Word.Language,
                    Translations = x.Word.Translations,
                    Value = x.Word.Value
                }).ToListAsync();

            return words;
        }

        public async Task<List<Word>> GetOptionsWordsAsync()
        {
            var words = await _db.Words
                .Where(x => x.Language == Language.Russian)
                .ToListAsync();

            return words;
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

        public async Task<Word> CreateAsync(Word newWord)
        {
            _db.Words.Add(newWord);
            await _db.SaveChangesAsync();
            return newWord;
        }
    }
}
