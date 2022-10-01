using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using System.Collections.Generic;

namespace GuessWord.IntegrationTests.Infrastructure.Persistence
{
    public static class SeedData
    {
        public const int UserId = 1;

        public static List<Word> GetWords()
        {
            var user = GetUser();
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

        public static User GetUser()
        {
            return new User { Id = UserId, Name = "Vova", Login = "Kola", Password = "2222" };
        }

        public static List<UserWord> GetUserWords()
        {
            var user = GetUser();
            var words = GetWords();

            return new List<UserWord>
            {
                new UserWord { User = user, Word = words[0], Complexity = 0, RepeatNumber = 3, TargetRepeatNumber = 2},
                new UserWord { User = user, Word = words[1], Complexity = 2, RepeatNumber = 3, TargetRepeatNumber = 2},
            };
        }
    }
}
