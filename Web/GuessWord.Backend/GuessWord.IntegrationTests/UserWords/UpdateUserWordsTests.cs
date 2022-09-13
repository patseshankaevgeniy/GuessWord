using GuessWord.IntegrationTests.Infrastructure.Client;
using GuessWord.IntegrationTests.Infrastructure.Persistence;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.IntegrationTests.UserWords
{
    public class UpdateUserWordsTests : IntegrationTest
    {
        [Fact]
        public async Task Update_ShouldReturn204()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            var userWordPatchDto = new UserWordPatchDto { Status = 2 };

            // Act
            var response = await ApiClient.UpdateUserWordAsync(expectedUserWords[0].Id, userWordPatchDto);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, (HttpStatusCode)response.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturn400_IfIdIsInvalid()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            var userWordPatchDto = new UserWordPatchDto { Status = 2 };

            // Act
            var task = ApiClient.UpdateUserWordAsync(0, userWordPatchDto);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)exeption.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturn400_IfStatusIsInvalid()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            var userWordPatchDto = new UserWordPatchDto { Status = 2 };

            // Act
            var task = ApiClient.UpdateUserWordAsync(1, null);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)exeption.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturn400_IfUserWordIsExist()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            var userWordPatchDto = new UserWordPatchDto { Status = 2 };

            // Act
            var task = ApiClient.UpdateUserWordAsync(4, userWordPatchDto);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)exeption.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturn403_IfUserIdIsNotTrue()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(2);

            var userWordPatchDto = new UserWordPatchDto { Status = 2 };

            // Act
            var task = ApiClient.UpdateUserWordAsync(1, userWordPatchDto);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal(HttpStatusCode.Forbidden, (HttpStatusCode)exeption.StatusCode);
        }
    }
}
