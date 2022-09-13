using GuessWord.IntegrationTests.Infrastructure.Client;
using GuessWord.IntegrationTests.Infrastructure.Persistence;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.IntegrationTests.UserWords
{
    public class DeleteUserWordsTests : IntegrationTest
    {
        [Fact]
        public async Task Delete_ShouldReturn400_IfUserWordsDtoIdIsInvalid()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            // Act
            var task = ApiClient.DeleteUserWordAsync(0);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)exeption.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn403IfUserWordHavesOtherId()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            // Act
            var task = ApiClient.DeleteUserWordAsync(1);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal(HttpStatusCode.Forbidden, (HttpStatusCode)exeption.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturn204()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            // Act
            var task = await ApiClient.DeleteUserWordAsync(expectedUserWords[0].Id);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, (HttpStatusCode)task.StatusCode);
        }
    }
}
