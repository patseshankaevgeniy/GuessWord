using GuessWord.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.DataAccess
{
    public static class SeedData
    {
        public static async Task AddDefaultUsersAsync(ApplicationDbContext db)
        {
            if (await db.Users.AnyAsync())
            {
                return;
            }

            var adminRole = new Role { Name = "admin" };
            var userRole = new Role { Name = "user" };

            var users = new List<User>
            {
                new User
                {
                    Name = "Dzhon",
                    Login = "Fanta",
                    Password = "1234",
                    UserRoles = new List<UserRole>
                    {
                        new UserRole{ Role = adminRole },
                        new UserRole{ Role = userRole }
                    }
                },
                new User
                {
                    Name = "Vlad",
                    Login = "Ice",
                    Password = "4444",
                    UserRoles = new List<UserRole>
                    {
                        new UserRole{ Role = userRole }
                    }
                }
            };

            db.Users.AddRange(users);
            await db.SaveChangesAsync();
        }
    }
}
