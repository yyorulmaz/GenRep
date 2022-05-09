using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GenRep.Contract
{
    public interface IRepository<T> where T : class, new()
    {
        #region SELECT
        Task<T> SelectAsync(object id, CancellationToken cancellationToken = default);
        Task<T> SelectAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
        Task<List<T>> SelectAllAsync(Expression<Func<T, bool>> filter = null, CancellationToken cancellationToken = default);
        #endregion

        #region INSERT
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task<List<T>> InsertRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
        #endregion

        #region UPDATE
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<List<T>> UpdateRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
        #endregion

        #region DELETE
        Task<T> DeleteAsync(object id, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
        #endregion
    }
}
