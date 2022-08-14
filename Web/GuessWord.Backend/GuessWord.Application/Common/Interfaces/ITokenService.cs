using GuessWord.Domain.Entities;

namespace GuessWord.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(User user);
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
