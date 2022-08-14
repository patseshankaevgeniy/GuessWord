using GuessWord.Application.Common.Interfaces.Repositories;
using GuessWord.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace GuessWord.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public User GetByLogin(string login)
        {
            var user = _db.Users
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstOrDefault(x => x.Login == login);

            return user;
        }

        public int AddNewUser(string name, string login, string password)
        {
            var user = new User { Login = login, Name = name, Password = password };

            var task = _db.Users.Add(user);
            _db.SaveChanges();

            return user.Id;
        }
    }
}
