using GuessWord.Mobile.Application.UserWords.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.UserWords.Services
{
    public class FakeUserWordService : IUserWordService, IUserWordEditService
    {
        List<UserWord> userWords = new List<UserWord>
            {
                new UserWord{Status = "Done", Translations = "Дом", Word= "Home", Id = 5},
                new UserWord{Status = "In Progress", Translations = "Работа", Word= "Work", Id = 7},
                new UserWord{Status = "In Progress", Translations = "Солнце", Word= "Sun", Id = 8},
                new UserWord{Status = "Done", Translations = "Звезда", Word= "Star", Id = 9},
                new UserWord{Status = "In Progress", Translations = "Доска", Word= "Board", Id = 13 },
                new UserWord{Status = "Done", Translations = "Телефон, Дом, Тарелка", Word= "Phone", Id = 1},
            };

        public FakeUserWordService()
        {

        }

        public async Task<List<UserWord>> GetAllAsync()
        {
            return userWords;
            //return null;
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
            var userWord = userWords.FirstOrDefault(x => x.Id == id);
            if (userWord == null)
            {
                throw new Exception("You don't have this word");
            }
            userWords.Remove(userWord);

            return Task.CompletedTask;
        }

        public Task<UserWord> CreateAsync(string word)
        {
            throw new NotImplementedException();
        }

        Task<UserWordEdit> IUserWordEditService.GetAsync(int id)
        {
            var userWord = new UserWord();
            for (int i = 0; i < userWords.Count; i++)
            {
                if (userWords[i].Id == id)
                {
                    userWord = userWords[i];
                };
            }

            if (userWord == null)
            {
                throw new Exception("You don't have this word");
            }

            var userWordEdit = new UserWordEdit
            {
                Id = userWord.Id,
                Status = userWord.Status,
                Word = userWord.Word,
                Translations = new Collection<Translation>()
            };

            var listTranslations = userWord.Translations.Split(',').ToList();
            foreach (var translation in listTranslations)
            {
                userWordEdit.Translations.Add(new Translation
                {
                    DeleteVisible = true,
                    EditVisible = true,
                    Value = translation
                });
            }

            return Task.FromResult(userWordEdit);
        }

        public Task<UserWord> UpdateAsync(UserWord userWord)
        {
            if (userWord != null)
            {
                for (int i = 0; i < userWords.Count; i++)
                {
                    if (userWords[i].Word == userWord.Word)
                    {
                        userWords[i] = userWord;
                        return Task.FromResult(userWords[i]);
                    }
                }
            }
            return null;
        }


    }
}
