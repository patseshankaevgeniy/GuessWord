using GuessWord.Domain.Entities;
using System.Collections.Generic;

namespace GuessWord.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User GetByLogin(string login);
        int AddNewUser(string name, string login, string password);
    }
}
