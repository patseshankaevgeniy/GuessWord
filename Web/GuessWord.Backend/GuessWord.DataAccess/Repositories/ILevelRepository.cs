using GuessWord.Domain.Entities;
using System.Collections.Generic;

namespace GuessWord.DataAccess.Repositories
{
    public interface ILevelRepository
    {
        List<Word> GetWordsByStatus(string status);
    }
}