using GuessWord.Api.Models;
using GuessWord.BusinessLogic.Services;
using GuessWord.BusinessLogic.Services.Interfaces;
using GuessWord.DataAccess.Repositories;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.Tests
{
    public class LevelsServiceTests
    {
        private readonly LevelsService _levelsService;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<IWordsRepository> _mockWordsRepository;

        public LevelsServiceTests()
        {
            _mockCurrentUserService = new Mock<ICurrentUserService>();
            _mockWordsRepository = new Mock<IWordsRepository>();

            _levelsService = new LevelsService(_mockWordsRepository.Object, _mockCurrentUserService.Object);
        }

        [Fact]

        public async Task GetTaskAsync_ShouldReturnLevel()
        {
            // Arrage

            _mockCurrentUserService
                .SetupGet(x => x.UserId)
                .Returns(5);

            _mockWordsRepository
                .Setup(x => x.GetWordsWithTranslation(It.IsAny<int>(), WordStatus.InProgress))
                .Returns(new List<WordWithTranslation>
                {
                    new WordWithTranslation {Value = "Слово",Translation = "word"},
                    new WordWithTranslation {Value = "Работа",Translation = "work"},
                    new WordWithTranslation {Value = "Дом",Translation = "home"},
                    new WordWithTranslation {Value = "Погода",Translation = "weather"}
                }) ;

            //_mockWordsRepository
            //    .Setup(x => x.GetOptionsWordsAsync(It.IsAny<int>()))
            //    .Returns(new List<Word>
            //    {
            //        new Word {Value = "one"},
            //        new Word {Value = "two"},
            //        new Word {Value = "only"},
            //        new Word {Value = "current"},
            //        new Word {Value = "level"},
            //        new Word {Value = "apple"},
            //        new Word {Value = "orange"},
            //        new Word {Value = "red"},
            //        new Word {Value = "world"},
            //    });

            // Act
            var actualLevel = await _levelsService.GetLevelAsync();

            // Assert
            Assert.NotNull(actualLevel);
            Assert.Equal(2, actualLevel.Count);
            Assert.Equal(2, actualLevel.Steps.Count);
            Assert.Equal(3, actualLevel.Steps[0].Options.Count);
            Assert.Equal(3, actualLevel.Steps[1].Options.Count);
        }
    }
}
