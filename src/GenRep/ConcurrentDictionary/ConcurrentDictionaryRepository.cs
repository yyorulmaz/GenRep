using GenRep.Contract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenRep.General
{
    /// <summary>
    /// 
    /// </summary>
    public class ConcurrentDictionaryRepository<TKey, TValue> : IConcurrentDictionaryRepository<TKey, TValue>
            //where T : class, new()
        where TKey : notnull
        where TValue : notnull
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ConcurrentDictionary<TKey, TValue> _db;
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionaryRepository(ConcurrentDictionary<TKey, TValue> db)
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
        public ConcurrentDictionary<TKey, TValue> Data => _db;

        /// <summary>
        /// 
        /// </summary>
        public TValue TryGetValue(TKey key)
        {
            _db.TryGetValue(key, out var rtrn);
            return rtrn;
        }
        /// <summary>
        /// 
        /// </summary>
        public TValue TryGetValue(Func<TValue, bool> filter)
        {
            return _db.Values.FirstOrDefault(filter);
        }
        /// <summary>
        /// 
        /// </summary>
        public List<TValue> GetAll(Func<TValue, bool> filter = null)
        {
            if (filter == null)
                return _db.Values.ToList();
            else
                return _db.Values.Where(filter).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        public bool TryAdd(TKey key, TValue value)
        {
            var result = _db.TryAdd(key, value);
            Task.Run(() => ChangedAdded?.Invoke(result));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool TryUpdate(TKey key, TValue value)
        {
            var result = _db.TryUpdate(key, value, value);
            Task.Run(() => ChangedUpdated?.Invoke(result));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        public TValue TryRemove(TKey key)
        {
            _db.TryRemove(key, out var rtrn);
            Task.Run(()=> ChangedRemoved?.Invoke(true));
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
        public event Action<bool> ChangedRemoved;
    }
}
