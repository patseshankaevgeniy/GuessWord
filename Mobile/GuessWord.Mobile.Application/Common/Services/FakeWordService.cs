﻿using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.Common.Services
{
    public class FakeWordService : IWordService
    {
        private readonly IGuessWordApiClient _apiClient;

        List<Models.Word> words = new List<Models.Word>
           {
               new Models.Word { Id = 1, Value = " Beat", Translations = "Дом" },
               new Models.Word { Id = 1, Value = " Bear", Translations = "Дом" },
               new Models.Word { Id = 1, Value = " Be", Translations = "Дом" },
               new Models.Word { Id = 1, Value = " Become", Translations = "Дом" },
               new Models.Word { Id = 1, Value = " Begin", Translations = "Дом, пол, гол" },
               new Models.Word { Id = 1, Value = " Bind", Translations = "Дом, да, два" },
               new Models.Word { Id = 1, Value = " Bite", Translations = "Дом, ура, туда" },
               new Models.Word { Id = 1, Value = " Bleed", Translations = "Дом" },
               new Models.Word { Id = 1, Value = " Blow", Translations = "Дом" },
               new Models.Word { Id = 1, Value = " Home", Translations = "Дом" },
               new Models.Word { Id = 1, Value = " Horse", Translations = "Дом" },
           };

        public FakeWordService(IGuessWordApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<List<Word>> GetAllAsync()
        {
            //var words = await _apiClient.GetWordsAsync();

            return words.Select(x => new Models.Word
            {
                Id = x.Id,
                Value = x.Value,
                //Translations = x.Translations.Aggregate((x, y) =>
                //                                         x.Length == 1 ?
                //                                         x + y : x + ", " + y),
            })
                .ToList();
        }

        public async Task<List<Models.Word>> GetByLetterAsync(string letter)
        {
           
            return words.Where(x => x.Value.ToLower().Contains(letter)).ToList();
        }

        public async Task<Models.Word> GetAsync(string value)
        {
           // var wordDto = await _apiClient.GetWordAsync(value);

            var word = new Models.Word
            {
                Id = 1,
                Value = value,
                Translations = "ldf"
            };

            return word;
        }

        public async Task<Models.Word> CreateAsync(string value)
        {
            var wordDto = await _apiClient.CreateWordAsync(value);

            var word = new Models.Word
            {
                Id = wordDto.Id,
                Value = wordDto.Value,
                Translations = wordDto.Translations.Aggregate((aggregate, value) =>
                                                                 aggregate.Length == 1 ?
                                                                 aggregate + value : aggregate + ", " + value)
            };

            return word;
        }


        public async Task DeleteAsync(int id)
        {
            // await _apiClient.(id);
        }

    }
}