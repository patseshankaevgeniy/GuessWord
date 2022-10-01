using GuessWord.Api;
using GuessWord.Application.Common.Interfaces;
using GuessWord.IntegrationTests.Infrastructure.Client;
using GuessWord.IntegrationTests.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GuessWord.IntegrationTests
{
    public abstract class IntegrationTest : IAsyncLifetime
    {
        private DbConnection dbConnection;
        private IServiceScope dbScope;
        private WebApplicationFactory<Startup> application;

        protected IGuessWordApiClient ApiClient { get; private set; }
        protected IApplicationDbContext DbContext { get; private set; }
        protected Mock<ICurrentUserService> MockCurrentUserService { get; private set; }

        public async Task InitializeAsync()
        {
            MockCurrentUserService = new Mock<ICurrentUserService>();

            application = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // Remove SqlServer DbContext
                        services
                            .RemoveAll(typeof(GuessWord.Persistence.ApplicationDbContext))
                            .RemoveAll(typeof(IApplicationDbContext))
                            .Remove(services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<GuessWord.Persistence.ApplicationDbContext>)));

                        // Add Sqlite DbContext
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseSqlite(CreateInMemoryDateBase());
                        });
                        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

                        // Replace ICurrentUserService
                        services.RemoveAll(typeof(ICurrentUserService));
                        services.AddScoped<ICurrentUserService>(_ => MockCurrentUserService.Object);
                    });
                });

            dbScope = application.Services.CreateScope();
            var dbContext = dbScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.EnsureCreatedAsync();
            DbContext = dbContext;

            var httpClient = application.CreateClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRmFudGF6ZXIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUiLCJleHAiOjE2NjE2Nzc3MTYsImlzcyI6IlRlc3QuY29tIiwiYXVkIjoiVGVzdC5jb20ifQ.49wftCzj7iBGapdsMW53Vu-rVtqD5Y80mqSyy2lqFww");
            ApiClient = new GuessWordApiClient(string.Empty, httpClient);
        }

        public async Task DisposeAsync()
        {
            dbScope.Dispose();
            if (dbConnection.State != ConnectionState.Closed)
            {
                await dbConnection.DisposeAsync();
            }

            //HttpClient.Dispose();
            await application.DisposeAsync();
        }

        public DbConnection CreateInMemoryDateBase()
        {
            if (dbConnection == null)
            {
                dbConnection = new SqliteConnection("Filename=:memory:");
                dbConnection.Open();
            }

            return dbConnection;
        }
    }
}
