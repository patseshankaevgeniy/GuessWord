using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.Common.Services;
using GuessWord.Mobile.Infrastructure.Clients;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GuessWord.Mobile.Infrastructure
{
    public static class DependencyInjections
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IGuessWordApiClient>(services =>
            {
                try
                {
                    var currentUserService = services.GetService<ICurrentUserService>();
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentUserService.AccessToken);
                    return new GuessWordApiClient("https://7715-178-172-234-92.ngrok.io/", httpClient);
                }
                catch (Exception)
                {

                    throw;
                }
            });

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<ICurrentUserService, FakeCurrentUserService>();
        }
    }
}
