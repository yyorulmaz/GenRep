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
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; }
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionary<TKey, TValue> Data { get; }
        /// <summary>
        /// 
        /// </summary>
        TValue TryGetValue(TKey key);
        /// <summary>
        /// 
        /// </summary>
        TValue TryGetValue(Func<TValue, bool> filter);
        /// <summary>
        /// 
        /// </summary>
        List<TValue> GetAll(Func<TValue, bool> filter = null);
        /// <summary>
        /// 
        /// </summary>
        bool TryAdd(TKey key, TValue value);
        /// <summary>
        /// 
        /// </summary>
        bool TryUpdate(TKey key, TValue value);
        /// <summary>
        /// 
        /// </summary>
        TValue TryRemove(TKey key);


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
    }
}