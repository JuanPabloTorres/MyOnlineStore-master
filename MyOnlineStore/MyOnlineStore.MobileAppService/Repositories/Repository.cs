using Microsoft.EntityFrameworkCore;
using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyOnlineStore.MobileAppService.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MyContext _Context;

        public Repository(MyContext context)
        {
            _Context = context;
        }

        public virtual bool Add(TEntity entity)
        {
            bool added;
            bool saved;

            try
            {
                var addedEntry = _Context.Set<TEntity>().Add(entity);
                var x = addedEntry.State;
                added = true;
                saved = Save();
            }
            catch (Exception ex) { Console.WriteLine($"Exception in Add() Repository: {ex.Message}"); return false; }
            
            return added && saved;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            bool added = false;
            bool saved = false;

            try
            {
                var addedEntry = _Context.Set<TEntity>().AddAsync(entity).Result;

                if (addedEntry.State == EntityState.Added)
                {
                    saved = Save();
                }
            }
            catch (Exception ex) { Console.WriteLine($"Exception in Add() Repository: {ex.Message}"); return false; }

            return added && saved;
        }

        public virtual bool AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _Context.Set<TEntity>().AddRange(entities);
            }
            catch (Exception) { return false; }

            return true;
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _Context.Set<TEntity>().Where(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var list = _Context.Set<TEntity>().ToList();
            return list;
        }

        public virtual TEntity Get(Guid id)
        {
            var entity = _Context.Set<TEntity>().Find(id);
            return entity;
        }

        public virtual bool Remove(TEntity entity)
        {
            try
            {
                _Context.Set<TEntity>().Remove(entity);
            }
            catch (Exception) { return false; }

            return true;
        }

        public virtual bool RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
               _Context.Set<TEntity>().RemoveRange(entities);
            }
            catch (Exception) { return false; }

            return true;
        }

        public bool Save()
        {
            bool saved;
            try
            {
                saved = _Context.SaveChanges() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Repository Save: { ex.Message}");
                saved = false;
            }

            return saved;          
        }
        public async Task<bool> SaveAsync()
        {
            bool saved;
            try
            {
                saved = await _Context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Repository Save: { ex.Message}");
                saved = false;
            }

            return saved;
        }

        public bool Update(TEntity entity)
        {
            bool added;
            bool saved;
            try
            {
               //var _entity = _Context.Set<TEntity>().
                //_Context.Entry(entity).CurrentValues.SetValues(entity);
                var updatedEntity = _Context.Set<TEntity>().Update(entity);
                //var x = updatedEntity.State;
                added = true;
            }
            catch (Exception ex)
            {
                added = false;
                Console.WriteLine($"Exception in Repository: {ex.Message}");
            }

            saved = Save();

            return added && saved;
        }
    }
}
