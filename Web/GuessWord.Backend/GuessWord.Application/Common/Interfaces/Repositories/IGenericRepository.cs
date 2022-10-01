using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GuessWord.Application.Common.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> CreateAsync(TEntity item, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> UpdateAsync(TEntity item);
        Task DeleteAsync(TEntity item);
    }
}
