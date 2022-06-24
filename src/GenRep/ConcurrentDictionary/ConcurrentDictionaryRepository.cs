using GenRep.Contract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenRep.General
{
    /// <summary>
    /// 
    /// </summary>
    public class ConcurrentDictionaryRepository<T> : IConcurrentDictionaryRepository<T>
            //where T : class, new()
        where T : notnull
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ConcurrentDictionary<string, T> _db;
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionaryRepository(ConcurrentDictionary<string, T> db)
        {
            _db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count => _db.Count;

        /// <summary>
        /// 
        /// </summary>
        public T TryGetValue(string key)
        {
            _db.TryGetValue(key, out var rtrn);
            return rtrn;
        }
        /// <summary>
        /// 
        /// </summary>
        public T TryGetValue(Func<T, bool> filter)
        {
            return _db.Values.FirstOrDefault(filter);
        }
        /// <summary>
        /// 
        /// </summary>
        public List<T> GetAll(Func<T, bool> filter = null)
        {
            if (filter == null)
                return _db.Values.ToList();
            else
                return _db.Values.Where(filter).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        public bool TryAdd(string key, T value)
        {
            var result = _db.TryAdd(key, value);
            ChangedAdded?.Invoke(result);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool TryUpdate(string key, T value)
        {
            var result = _db.TryUpdate(key, value, value);
            ChangedUpdated?.Invoke(result);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        public T TryRemove(string key)
        {
            _db.TryRemove(key, out var rtrn);
            ChangedDeleted?.Invoke(true);
            return rtrn;
        }

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
