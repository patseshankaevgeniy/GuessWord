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

        public bool AddUserWord(Word word)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserWord(Word word)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserWord>> GetUserWordsAsync(int userId)
        {
            var userWords = await _db.UsersWords
                .Include(x => x.Word)
                .ThenInclude(x => x.Translations)
                .ThenInclude(x => x.Translation)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return userWords;
        }

        public bool UpdateUserWord(Word word)
        {
            throw new NotImplementedException();
        }
    }
}
