using GuessWord.IntegrationTests.Infrastructure.Persistence;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.IntegrationTests.Words
{
    public class GetWordsTests : IntegrationTest
    {
        [Fact]
        public async Task Get_ShouldReturn200AndWordsWithH()
        {
            // Arrange
            var expectedWords = SeedData.GetWords();
            DbContext.Words.AddRange(expectedWords);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await ApiClient.GetWordsAsync("H");

            // Assert
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);

            var words = response.Result.ToList();
            Assert.Single(words);
            Assert.Equal(expectedWords[0].Value, words[0].Value);
        }

        [Fact]
        public async Task Get_ShouldReturn200AndAllWords()
        {
            // Arrange
            var expectedWords = SeedData.GetWords();
            DbContext.Words.AddRange(expectedWords);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
               .SetupGet(x => x.UserId)
               .Returns(SeedData.UserId);

            // Act
            var response = await ApiClient.GetWordsAsync(null);

            // Assert
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)response.StatusCode);

            var words = response.Result.ToList();
            Assert.Equal(2, words.Count);
            Assert.Equal(expectedWords[0].Value, words[0].Value);
            Assert.Equal(expectedWords[1].Value, words[1].Value);
        }
    }
}
