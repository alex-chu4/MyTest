using EMIC2.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Util.WebClass;

namespace EMIC2.Models.Repository
{
    /// <summary>
    /// 實作Entity Framework Generic Repository 的 Class。
    /// </summary>
    /// <typeparam name="TEntity">EF Model 裡面的Type</typeparam>
    public class UnitOfWorkRepository<TEntity> : IUnitOfWorkRepository<TEntity>
        where TEntity : class
    {
        private DbContext Context { get; set; }
        DBDataUtility util = new DBDataUtility();

        /// <summary>
        /// 建構EF一個Entity的Repository，需傳入此Entity的Context。
        /// </summary>
        /// <param name="inContext">Entity所在的Context</param>
        public UnitOfWorkRepository(DbContext inContext)
        {
            Context = inContext;
        }

        /// <summary>
        /// 新增一筆資料到資料庫。
        /// </summary>
        /// <param name="entity">要新增到資料的庫的Entity</param>
        public void Create(TEntity entity)
        {
            // fortify 20191101 : Access Control: Database
            Context.Set<TEntity>().Add((TEntity)util.CheckNull(entity));
        }

        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>符合條件資料的IQueryable。</returns>
        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).AsQueryable();
        }


        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// 更新一筆Entity內容。
        /// </summary>
        /// <param name="instance">要更新的內容</param>
        public void Update(TEntity instance)
        {
            Context.Entry<TEntity>(instance).State = EntityState.Modified;
        }

        /// <summary>
        /// 更新一筆Entity的內容，只更新有指定的Property。
        /// </summary>
        /// <param name="instance">要更新的內容。</param>
        /// <param name="properties">需要更新的欄位。</param>
        public void Update(TEntity instance, params Expression<Func<TEntity, object>>[] properties)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            Context.Configuration.ValidateOnSaveEnabled = false;

            Context.Entry<TEntity>(instance).State = EntityState.Unchanged;

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    Context.Entry<TEntity>(instance).Property(property).IsModified = true;
                }
            }
        }

        /// <summary>
        /// 更新一筆Entity的內容，更新整筆資料。
        /// </summary>
        /// <param name="instance">要更新的內容。</param>
        /// <param name="keyValues">需要更新資料的Key值。</param>
        public void Update(TEntity instance, params object[] keyValues)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            var entry = Context.Entry<TEntity>(instance);

            if (entry.State == EntityState.Detached)
            {
                var set = Context.Set<TEntity>();

                // fortify 20191101 : Access Control: Database
                TEntity attachedEntity = set.Find((object[])util.CheckNull(keyValues));

                if (attachedEntity != null)
                {
                    var attachedEntry = Context.Entry(attachedEntity);
                    // fortify 20191101 : Access Control: Database
                    attachedEntry.CurrentValues.SetValues(util.CheckNull(instance));
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        public void Delete(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Deleted;
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
                this.Context.Set<TEntity>().AddRange((IEnumerable<TEntity>)util.CheckNull(instances));
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
                this.Context.Set<TEntity>().RemoveRange((IEnumerable<TEntity>)util.CheckNull(instances));
            }
        }

        /// <summary>
        /// 儲存異動。
        /// </summary>
        public void SaveChanges()
        {
            Context.SaveChanges();

            // 因為Update 單一model需要先關掉validation，因此重新打開
            if (Context.Configuration.ValidateOnSaveEnabled == false)
            {
                Context.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        /// <summary>
        /// 非同步儲存異動。
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();

            // 因為Update 單一model需要先關掉validation，因此重新打開
            if (!Context.Configuration.ValidateOnSaveEnabled)
            {
                Context.Configuration.ValidateOnSaveEnabled = true;
            }
        }
    }
}