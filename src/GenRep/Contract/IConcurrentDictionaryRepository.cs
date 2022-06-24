using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenRep.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConcurrentDictionaryRepository<T> 
        //where T : class, new()
        where T : notnull
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; }
        /// <summary>
        /// 
        /// </summary>
        T TryGetValue(string key);
        /// <summary>
        /// 
        /// </summary>
        T TryGetValue(Func<T, bool> filter);
        /// <summary>
        /// 
        /// </summary>
        List<T> GetAll(Func<T, bool> filter = null);
        /// <summary>
        /// 
        /// </summary>
        bool TryAdd(string key, T value);
        /// <summary>
        /// 
        /// </summary>
        bool TryUpdate(string key, T value);
        /// <summary>
        /// 
        /// </summary>
        T TryRemove(string key);


        /// <summary>
        /// 
        /// </summary>
        public event Action<bool> ChangedAdded;
        /// <summary>
        /// 
        /// </summary>
        public event Action<bool> ChangedUpdated;
        /// <summary>
        /// 
        /// </summary>
        public event Action<bool> ChangedDeleted;
    }
}