using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface
{
    /// <summary>
    /// 代表一個Repository的interface。
    /// </summary>
    /// <typeparam name="TEntity">任意model的class</typeparam>
    public interface IUnitOfWorkRepository<TEntity>
    {
        /// <summary>
        /// 新增一筆資料。
        /// </summary>
        /// <param name="instance">要新增到的Entity</param>
        void Create(TEntity instance);

        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>符合條件資料的IQueryable。</returns>
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 更新一筆Entity內容。
        /// </summary>
        /// <param name="instance">要更新的內容</param>
        void Update(TEntity instance);

        /// <summary>
        /// 更新一筆Entity內容，只更新有指定的Property。
        /// </summary>
        /// <param name="entity">要更新的內容。</param>
        /// <param name="properties">需要更新的欄位。</param>
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);

        /// <summary>
        /// 更新一筆Entity的內容，更新整筆資料。
        /// </summary>
        /// <param name="instance">要更新的內容。</param>
        /// <param name="keyValues">需要更新資料的Key值。</param>
        void Update(TEntity instance, params object[] keyValues);

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="instance">要被刪除的Entity。</param>
        void Delete(TEntity instance);

        void AddRange(IEnumerable<TEntity> instances);

        void RemoveRange(IEnumerable<TEntity> instances);

        /// <summary>
        /// 儲存異動。
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// 非同步儲存異動。
        /// </summary>
        Task SaveChangesAsync();
    }
}