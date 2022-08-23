using GuessWord.Persistence;
using System.Collections.Generic;

namespace GuessWord.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserWord> UserWords { get; set; }
    }
}
