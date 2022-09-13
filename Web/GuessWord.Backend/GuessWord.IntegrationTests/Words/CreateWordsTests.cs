using GuessWord.IntegrationTests.Infrastructure.Client;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.IntegrationTests.Words
{
    public class CreateWordsTests : IntegrationTest
    {
        [Fact]
        public async Task Create_ShouldReturn400_IfWordsDtoIsInvalid()
        {
            // Arrange
            var wordDto = new WordDto
            {
                Language = -200,
                Value = null,
                Translations = null
            };

            // Act
            var task = ApiClient.CreateWordAsync(wordDto);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)exeption.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturn201AndWordId()
        {
            // Arrange
            var wordDto = new WordDto
            {
                Language = 0,
                Value = "House",
                Translations = new List<string> { "Дом" }
            };

            // Act
            var response = await ApiClient.CreateWordAsync(wordDto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)response.StatusCode);
        }
    }
}
