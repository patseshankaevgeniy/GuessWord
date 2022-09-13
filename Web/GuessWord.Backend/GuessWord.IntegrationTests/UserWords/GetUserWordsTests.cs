using GuessWord.IntegrationTests.Infrastructure.Persistence;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.IntegrationTests.UserWords
{
    public class GetUserWordsTests : IntegrationTest
    {
        [Fact]
        public async Task Get_ShouldReturn200AndAllUserWords()
        {
            // Arrange
            var expectedUserWords = SeedData.GetUserWords();
            DbContext.UsersWords.AddRange(expectedUserWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            // Act
            var response = await ApiClient.GetUserWordsAsync();
            var userWords = response.Result.ToList();

            // Assert
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);
            Assert.Equal(2, userWords.Count);
            Assert.Equal(expectedUserWords[0].Id, userWords[0].Id);
            Assert.Equal(expectedUserWords[1].Id, userWords[1].Id);
            Assert.Equal(expectedUserWords[0].Word.Value, userWords[0].Word);
            Assert.Equal(expectedUserWords[1].Word.Value, userWords[1].Word);
        }
    }
}
