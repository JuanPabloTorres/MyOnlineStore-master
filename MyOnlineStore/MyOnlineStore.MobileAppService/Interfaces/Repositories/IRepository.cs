using MyOnlineStore.Entities.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        bool Add(TEntity entity);
        Task<bool> AddAsync(TEntity entity);
        bool AddRange(IEnumerable<TEntity> entities);

        bool Remove(TEntity entity);
        bool RemoveRange(IEnumerable<TEntity> entities);
        bool Update(TEntity entity);
        bool Save();
        Task<bool> SaveAsync();
    }
}
