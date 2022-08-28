using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;

namespace GuessWord.IntegrationTests.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static List<Word> GetWords()
        {
            return new List<Word>
            {
                new Word
                {
                    Value = "Home",
                    Language = Language.English,
                    Translations = new List<WordTranslation>
                    {
                        new WordTranslation{Translation = new Word{Value = "дом", Language = Language.Russian}}
                    }
                },
                new Word
                {
                    Value = "Class",
                    Language = Language.English,
                    Translations = new List<WordTranslation>
                    {
                        new WordTranslation{Translation = new Word{Value = "класс", Language = Language.Russian}}
                    }
                }
            };
        }
    }
}
