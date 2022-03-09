using GuessWord.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
