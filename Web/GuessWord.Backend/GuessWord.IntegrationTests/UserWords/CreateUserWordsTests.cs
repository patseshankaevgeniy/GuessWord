using GuessWord.IntegrationTests.Infrastructure.Client;
using GuessWord.IntegrationTests.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.IntegrationTests.UserWords
{
    public class CreateUserWordsTests : IntegrationTest
    {
        [Fact]
        public async Task Create_ShouldReturn400_IfUserWordsDtoIsInvalid()
        {
            // Arrange
            var userWordDto = new UserWordDto
            {
                Status = -200,
                Word = null,
                Language = 2,
                Translations = null
            };

            // Act
            var task = ApiClient.CreateUserWordAsync(userWordDto);

            // Assert
            var exeption = await Assert.ThrowsAsync<ApiException<ApiErrorDto>>(() => task);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)exeption.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturn201AndUserWordId()
        {
            // Arrange
            var user = SeedData.GetUser();
            DbContext.Users.AddRange(user);
            await DbContext.SaveChangesAsync();

            MockCurrentUserService
                .SetupGet(x => x.UserId)
                .Returns(user.Id);

            var userWordDto = new UserWordDto
            {
                Language = 0,
                Word = "House",
                Translations = new List<string> { "Дом" }
            };

            // Act
            var response = await ApiClient.CreateUserWordAsync(userWordDto);
            var actualUserWord = response.Result;

            // Assert
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)response.StatusCode);

            var userWord = await DbContext.UsersWords
                .Include(x => x.Word).ThenInclude(x => x.Translations)
                .FirstAsync(x => x.Id == actualUserWord.Id);

            Assert.Equal(userWord.Word.Value, actualUserWord.Word);
            Assert.Equal((int)userWord.Word.Language, actualUserWord.Language);
            Assert.Equal(userWord.Word.Translations.Count, actualUserWord.Translations.Count);
        }
    }
}
