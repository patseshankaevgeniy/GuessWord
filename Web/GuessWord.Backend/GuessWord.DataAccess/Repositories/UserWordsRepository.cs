using GuessWord.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.DataAccess.Repositories
{
    public class UserWordsRepository : IUserWordsRepository
    {
        private readonly ApplicationDbContext _db;

        public UserWordsRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<List<UserWord>> GetAllAsync(int userId)
        {
            var userWords = await _db.UsersWords
                .Include(x => x.Word)
                .ThenInclude(x => x.Translations)
                .ThenInclude(x => x.Translation)
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return userWords;
        }

        public async Task<UserWord> GetAsync(int id)
        {
            var userWord = await _db.UsersWords
                .Include(x => x.Word)
                .ThenInclude(x => x.Translations)
                .ThenInclude(x => x.Translation)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

                return userWord;
        }

        public async Task<UserWord> CreateAsync(UserWord userWord)
        {
            _db.UsersWords.Add(userWord);
            _db.SaveChanges();

            userWord = await _db.UsersWords
                .Include(x => x.Word)
                .ThenInclude(x => x.Translations)
                .ThenInclude(x => x.Translation)
                .FirstOrDefaultAsync(u => u.Id == userWord.Id);

            return userWord;
        }

        public async Task<UserWord> UpdateAsync(UserWord userWord)
        {
            var word = _db.UsersWords.Update(userWord);
            await _db.SaveChangesAsync();
            return word.Entity;
        }

        public async Task RemoveAsync(UserWord userWord)
        {
            _db.UsersWords.Remove(userWord);
            await _db.SaveChangesAsync();
        }
    }
}
