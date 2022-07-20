using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenRep.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConcurrentDictionaryRepository<TKey, TValue> 
        //where T : class, new()
        where TKey : notnull
        where TValue : notnull
    {
        #region Data
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionary<TKey, TValue> Data { get; }
        #endregion

        #region Count
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; }
        #endregion

        #region CRUD
        /// <summary>
        /// 
        /// </summary>
        TValue Get(TKey key);
        /// <summary>
        /// 
        /// </summary>
        TValue Get(Func<TValue, bool> filter);
        /// <summary>
        /// 
        /// </summary>
        List<TValue> GetAll(Func<TValue, bool> filter = null);
        /// <summary>
        /// 
        /// </summary>
        bool Add(TKey key, TValue value);
        /// <summary>
        /// 
        /// </summary>
        bool Update(TKey key, TValue value);
        /// <summary>
        /// 
        /// </summary>
        TValue Remove(TKey key);
        #endregion

        #region Changed
        /// <summary>
        /// 
        /// </summary>
        public event Action<TValue> ChangedAdded;
        /// <summary>
        /// 
        /// </summary>
        public event Action<TValue> ChangedUpdated;
        /// <summary>
        /// 
        /// </summary>
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}