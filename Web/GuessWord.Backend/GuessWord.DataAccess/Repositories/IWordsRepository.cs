using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;

namespace GuessWord.DataAccess.Repositories
{
    public interface IWordsRepository
    {
        List<WordWithTranslation> GetWordsWithTranslation(int userId, WordStatus status);
        List<Word> GetOptionsWords(int userId);
    }
}