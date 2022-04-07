using GuessWord.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GuessWord.DataAccess.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        public List<Word> GetWordsByStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}
