using GuessWord.Mobile.Models;
using GuessWord.Mobile.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.Mobile.ViewModels.Test
{
    public class UserWordViewModelTests
    {
        private readonly UserWordViewModel _viewModel;
        private readonly Mock<IUserWordService> _mockUserWordService;
        private readonly Mock<INavigationService> _mockNavigationService;
       

        public UserWordViewModelTests()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockUserWordService = new Mock<IUserWordService>();
            _viewModel = new UserWordViewModel(_mockNavigationService.Object, _mockUserWordService.Object);
        }

        [Fact]
        public async Task OnInitializedAsync_ShouldLoadUserWords()
        {
            // Arrange
            var expectedUserWords = new List<UserWord>
            {
                new UserWord { Id = 1, Status = "Done", Translations = "Работать", Word = "Work"}
            };

            _mockUserWordService
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => expectedUserWords);

            // Act
            await _viewModel.OnInitializedAsync();

            // Assert
            Assert.Equal(expectedUserWords, _viewModel.UserWords);
            _mockUserWordService.Verify(x => x.GetAllAsync(), Times.Once());
        }
    }
}
