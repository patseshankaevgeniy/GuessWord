using GuessWord.Mobile.Clients;
using GuessWord.Mobile.Services;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.Mobile.Test.Services
{
    public class UserWordServiceTest
    {
        private readonly UserWordService _userWordService;
        private readonly Mock<IGuessWordApiClient> _mockGuessWordApiClient;
        private readonly Mock<FakeCurrentUserService> _mockCurrentUserService;

        public UserWordServiceTest()
        {
            _mockCurrentUserService = new Mock<FakeCurrentUserService>();
            _mockGuessWordApiClient = new Mock<IGuessWordApiClient>();
            _userWordService = new UserWordService(_mockGuessWordApiClient.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnUserWords()
        {
            // Arrange
            var userWordDtos = new List<UserWordDto>
            {
                new UserWordDto
                {
                    Id = 1,
                    Status = 0,
                    Translations = new []{ "Работать", "Работа" },
                    Word = "Work"
                }
            };

            _mockGuessWordApiClient
                .Setup(x => x.GetUserWordsAsync(CancellationToken.None))
                .ReturnsAsync(() => userWordDtos);

            // Act
            var userWords = await _userWordService.GetAllAsync();

            // Assert
            Assert.Single(userWords);
            Assert.Equal(userWordDtos[0].Id, userWords[0].Id);
            Assert.Equal(userWordDtos[0].Word, userWords[0].Word);
            Assert.Equal("New", userWords[0].Status);
            Assert.Equal("Работать, Работа", userWords[0].Translations);
        }
    }
}
