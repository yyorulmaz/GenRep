using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GenRep.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepository<T> where T : class, new()
    {
        #region SELECT
        /// <summary>
        /// 
        /// </summary>
        Task<T> SelectByIdAsync(object id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        Task<T> SelectAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        Task<List<T>> SelectAllAsync(Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        Task<T> OrderBy(Expression<Func<T, object>> filter, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        Task<List<T>> OrderByList(Expression<Func<T, object>> filter, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        Task<T> OrderByDescending(Expression<Func<T, object>> filter, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        Task<List<T>> OrderByDescendingList(Expression<Func<T, object>> filter, CancellationToken cancellationToken = default);
        #endregion

        #region INSERT
        /// <summary>
        /// 
        /// </summary>
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        Task<List<T>> InsertRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
        #endregion

        #region UPDATE
        /// <summary>
        /// 
        /// </summary>
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        Task<List<T>> UpdateRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
        #endregion

        #region DELETE
        /// <summary>
        /// 
        /// </summary>
        Task<T> DeleteAsync(object id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 
        /// </summary>
        Task<bool> DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
        #endregion
    }
}
