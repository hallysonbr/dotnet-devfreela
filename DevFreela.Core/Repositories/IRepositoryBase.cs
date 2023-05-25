using DevFreela.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task AddAsync(IEnumerable<TEntity> objs);
        void Update(TEntity obj);
        void UpdateRange(IEnumerable<TEntity> objs);
        void Remove(TEntity obj);
        void Remove(IEnumerable<TEntity> objs);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> where, bool tracking);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where, bool tracking);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where);
        Task<PaginationResult<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, int pageNumber, int pageSize);
        Task<PaginationResult<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, TKey>> order, int pageNumber, int pageSize);
    }
}
