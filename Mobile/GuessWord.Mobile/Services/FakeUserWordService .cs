using GuessWord.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class FakeUserWordService : IUserWordService
    {
        List<UserWord> userWords = new List<UserWord>
            {
                new UserWord{Status = "Done", Translations = "Дом", Word= "Home", Id = 5},
                new UserWord{Status = "In Progress", Translations = "Работа", Word= "Work", Id = 7},
                new UserWord{Status = "In Progress", Translations = "Солнце", Word= "Sun", Id = 8},
                new UserWord{Status = "Done", Translations = "Звезда", Word= "Star", Id = 9},
                new UserWord{Status = "In Progress", Translations = "Доска", Word= "Board", Id = 13 },
                new UserWord{Status = "Done", Translations = "Телефон", Word= "Phone", Id = 9},
            };

        public FakeUserWordService()
        {

        }

        public async Task<List<UserWord>> GetAllAsync()
        {
           return userWords;
           // return null;
        }

        public async Task<UserWord> GetAsync(int id)
        {
            for (int i = 0; i < userWords.Count; i++)
            {
                if (userWords[i].Id == id)
                {
                    return userWords[i];
                };
            }

            throw new Exception("Word is empty");
        }

        public Task<UserWord> CreateAsync(UserWord userWord)
        {
            userWords.Add(userWord);
            return Task.FromResult(userWord);
        }

        public Task<UserWord> UpdateAsync(UserWord userWord, int id)
        {
            for (int i = 0; i < userWords.Count; i++)
            {
                if (userWords[i].Id == id)
                {
                    userWords.Insert(i, userWord);
                    return Task.FromResult(userWords[i]);
                }
            }
            throw new Exception("Bad Request");
        }

        public Task DeleteAsync(int id)
        {
            for (int i = 0; i < userWords.Count; i++)
            {
                if (userWords[i].Id == id)
                {
                    userWords.Remove(userWords[i]);
                    return Task.CompletedTask;
                }
            }
            throw new Exception("You don't have this word");
        }

        public Task<UserWord> CreateAsync(string word)
        {
            throw new NotImplementedException();
        }

        public Task<UserWord> UpdateAsync(string status, int id)
        {
            throw new NotImplementedException();
        }
    }
}
