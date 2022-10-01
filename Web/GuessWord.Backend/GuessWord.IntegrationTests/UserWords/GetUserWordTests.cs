using GuessWord.IntegrationTests.Infrastructure.Client;
using GuessWord.IntegrationTests.Infrastructure.Persistence;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.IntegrationTests.UserWords
{
    public class GetUserWordTests : IntegrationTest
    {
        [Fact]
        public async Task Get_ShouldReturn200AndUserWord()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            // Act
            var response = await ApiClient.GetUserWordAsync(expectedUserWords[0].Id);
            var userWord = response.Result;

            // Assert
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
            Assert.Equal(expectedUserWords[0].Id, userWord.Id);
            Assert.Equal(expectedUserWords[0].Word.Value, userWord.Word);
        }

        [Fact]
        public async Task Get_ShouldReturn400IfIdIsInvalid()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            // Act
            var task = ApiClient.GetUserWordAsync(0);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)exeption.StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturn404IfIdIsNotExist()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            // Act
            var task = ApiClient.GetUserWordAsync(5);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal((int)HttpStatusCode.NotFound, exeption.StatusCode);
        }
    }
}
