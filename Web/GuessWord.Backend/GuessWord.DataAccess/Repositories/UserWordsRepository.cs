using GuessWord.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return userWords;
        }
      
        public Task<UserWord> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<UserWord> CreateAsync(UserWord userWord)
        {
            _db.UsersWords.Add(userWord);
            _db.SaveChanges();

            userWord = await _db.UsersWords
                .Include(x => x.Word)
                .ThenInclude(x => x.Translations)
                .ThenInclude(x=> x.Translation)
                .FirstOrDefaultAsync(u => u.Id == userWord.Id);
                
            return userWord;
        }

        public Task<UserWord> UpdateAsync(Word word)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
