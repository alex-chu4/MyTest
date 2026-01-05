using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace EMIC2.Models.Interface
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        bool Create(TEntity instance);

        bool Update(TEntity instance);

        bool Update(TEntity instance, params object[] keyValues);

        bool Update(TEntity instance, params Expression<Func<TEntity, object>>[] properties);

        bool Delete(TEntity instance);

        bool Delete(TEntity instance, params object[] keyValues);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        void AddRange(IEnumerable<TEntity> instances);

        void RemoveRange(IEnumerable<TEntity> instances);

        void SaveChanges();

        Task SaveChangesAsync();
    }
}