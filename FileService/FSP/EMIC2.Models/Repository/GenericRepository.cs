using EMIC2.Models.Helper;
using EMIC2.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Util.WebClass;

namespace EMIC2.Models.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private DbContext _context { get; set; }

        DBDataUtility util = new DBDataUtility();

        public GenericRepository()
            : this(DBHelper.CreateDbContext())
        {
        }

        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = new DbContext(context, true);
        }

        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public bool Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().Add((TEntity)util.CheckNull(instance));
                return (this.SaveChanges(true) > 0);
            }
        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Modified;
                return (this.SaveChanges(true) > 0);
            }
        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="keyValues">The key values.</param>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public bool Update(TEntity instance, params object[] keyValues)
        {
            if (keyValues == null)
            {
                return true;
            }

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            var entry = _context.Entry<TEntity>(instance);

            if (entry.State == EntityState.Detached)
            {
                var set = _context.Set<TEntity>();

                TEntity attachedEntity = set.Find((object[])util.CheckNull(keyValues));

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues((TEntity)util.CheckNull(instance));
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
            return (this.SaveChanges(true) > 0);
        }

        /// <summary>
        /// 只更新有指定的Property。
        /// </summary>
        /// <param name="entity">要更新的內容。</param>
        /// <param name="updateProperties">需要更新的欄位。</param>
        public bool Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            bool isUpdate = false;
            this._context.Configuration.ValidateOnSaveEnabled = false;

            this._context.Entry<TEntity>(entity).State = EntityState.Unchanged;

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    this._context.Entry<TEntity>(entity).Property(property).IsModified = true;
                }

                isUpdate = (this.SaveChanges(true) > 0);
            }

            return isUpdate;
        }

        /// <summary>
        /// Deletes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Deleted;
                return (this.SaveChanges(true) > 0);
            }
        }

        /// <summary>
        /// Deletes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="keyValues">The key values.</param>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public bool Delete(TEntity instance, params object[] keyValues)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            var entry = _context.Entry<TEntity>(instance);

            if (entry.State == EntityState.Detached)
            {
                var set = _context.Set<TEntity>();

                TEntity attachedEntity = set.Find(keyValues);

                if (attachedEntity != null)
                {
                    _context.Entry(attachedEntity).State = EntityState.Deleted;
                    //set.Remove(attachedEntity);
                }
                else
                {
                    entry.State = EntityState.Deleted;
                }
            }
            return (this.SaveChanges(true) > 0);
        }

        /// <summary>
        /// Gets the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return this._context.Set<TEntity>().AsQueryable();
        }

        public void SaveChanges()
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }

        private int SaveChanges(bool needReturnValue)
        {
            try
            {
                return this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                //do somthing

                return -1;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }

        /// <summary>
        /// Add Range
        /// </summary>
        /// <param name="instances"></param>
        public void AddRange(IEnumerable<TEntity> instances)
        {
            if (instances == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().AddRange(instances);
            }
        }

        /// <summary>
        /// Remove Range
        /// </summary>
        /// <param name="instances"></param>
        public void RemoveRange(IEnumerable<TEntity> instances)
        {
            if (instances == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().RemoveRange(instances);
            }
        }
    }
}