using GuessWord.Domain.Entities;
using System.Collections.Generic;

namespace GuessWord.Persistence
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}