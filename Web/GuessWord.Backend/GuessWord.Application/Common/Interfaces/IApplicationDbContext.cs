using GuessWord.Domain.Entities;
using GuessWord.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GuessWord.Application.Common.Interfaces
{
    public interface IApplicationDbContext 
    {
        DbSet<User> Users { get; set; }
        DbSet<Word> Words { get; set; }
        DbSet<UserWord> UsersWords { get; set; }
        DbSet<WordTranslation> Translations { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserRole> UserRoles { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}