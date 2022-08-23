﻿using GuessWord.Domain.Entities;

namespace GuessWord.Persistence
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}