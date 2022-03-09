using GuessWord.Domain.Entities;
using System.Collections.Generic;

namespace GuessWord.BusinessLogic.Services.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(User user);
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
