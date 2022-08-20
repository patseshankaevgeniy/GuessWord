﻿using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Application.Common.Interfaces.Repositories
{
    public interface IWordsRepository
    {
        Task<List<Word>> GetAllAsync();
        Task<List<Word>>GetByNameAsync(string value);
        Task<List<Word>> GetByLetterAsync(string letter);
        Task<List<Word>> GetOptionsWordsAsync();
        List<WordWithTranslation> GetWordsWithTranslation(int userId, WordStatus status);
        Task<Word> CreateAsync(Word newWord);
    }
}