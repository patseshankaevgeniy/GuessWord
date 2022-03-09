using GuessWord.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace GuessWord.Api
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var app = Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .Build();

            try
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                await db.Database.MigrateAsync();
                await SeedData.AddDefaultUsersAsync(db);
            }
            catch (Exception)
            {
                throw;
            }

            await app.RunAsync();
        }
    }
}