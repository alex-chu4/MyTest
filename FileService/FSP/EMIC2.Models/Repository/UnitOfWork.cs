using EMIC2.Models.Helper;
using EMIC2.Models.Interface;
using System;
using System.Collections;
using System.Data.Entity;
using System.Threading.Tasks;

namespace EMIC2.Models.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }
        private Hashtable _repositories;

        public UnitOfWork()
        {
            Context = DBHelper.CreateDbContext();
        }
        //public void Commit()
        //{
        //    Context.SaveChanges();
        //}

        public bool Commit()
        {
            try
            {
                return Context.SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                return false; 
            }
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="TEntity">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        public IUnitOfWorkRepository<TEntity> UoWRepository<TEntity>()
            where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(UnitOfWorkRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), Context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IUnitOfWorkRepository<TEntity>)_repositories[type];
        }
    }
}
