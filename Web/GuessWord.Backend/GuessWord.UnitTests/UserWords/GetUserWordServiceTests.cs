using GuessWord.Application.Common.Interfaces.Repositories;
using GuessWord.Application.Exceptions;
using GuessWord.Application.UserWords;
using GuessWord.Application.UserWords.Models;
using GuessWord.Domain.Entities;
using Moq;
using Moq.AutoMock;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.UnitTests.UserWords
{
    public class GetUserWordServiceTests
    {
        private readonly UserWordsService _userWordsService;
        private readonly AutoMocker _mocker;

        public GetUserWordServiceTests()
        {
            _mocker = new AutoMocker();
            _userWordsService = _mocker.CreateInstance<UserWordsService>();
        }

        [Fact]
        public async Task Get_ShouldReturnUserWordDtoById()
        {
            // Arrange
            var expectedUserWord = new UserWord
            {
                Id = 1,
                Complexity = 2,
            };

            _mocker
                .GetMock<IGenericRepository<UserWord>>()
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(() => expectedUserWord);

            _mocker
                .GetMock<IUserWordMapper>()
                .Setup(x => x.Map(It.IsAny<UserWord>()))
                .Returns(() => new UserWordDto())
                .Callback<UserWord>(actualUserWord =>
                {
                    // Assert
                    Assert.Equal(expectedUserWord, actualUserWord);
                });

            // Act
            var userWordDto = await _userWordsService.GetAsync(expectedUserWord.Id);

            // Assert
            Assert.NotNull(userWordDto);

            _mocker
                .GetMock<IGenericRepository<UserWord>>()
                .Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);

            _mocker
                .GetMock<IUserWordMapper>()
                .Verify(x => x.Map(It.IsAny<UserWord>()), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Get_ShouldThrowValidationException_IfIdIsNotValid(int id)
        {
            // Act
            var task = _userWordsService.GetAsync(id);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(() => task);

            _mocker
               .GetMock<IGenericRepository<UserWord>>()
               .Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);

            _mocker
               .GetMock<IUserWordMapper>()
               .Verify(x => x.Map(It.IsAny<UserWord>()), Times.Never);
        }

        [Fact]
        public async Task Get_ShouldThrowNotFoundException_IfUserWordNotFound()
        {
            // Arrange
            _mocker
                .GetMock<IGenericRepository<UserWord>>()
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act 
            var task = _userWordsService.GetAsync(2);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => task);

            _mocker
                .GetMock<IGenericRepository<UserWord>>()
                .Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);

            _mocker
                .GetMock<IUserWordMapper>()
                .Verify(x => x.Map(It.IsAny<UserWord>()), Times.Never);
        }
    }
}
