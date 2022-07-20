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
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionaryRepository(ConcurrentDictionary<TKey, TValue> data)
        {
            this.data = data;
        }
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionaryRepository()
        {
            this.data = new ConcurrentDictionary<TKey, TValue>();
        }
        #endregion

        #region Data
        /// <summary>
        /// 
        /// </summary>
        private readonly ConcurrentDictionary<TKey, TValue> data;
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionary<TKey, TValue> Data => data;
        #endregion

        #region Count
        /// <summary>
        /// 
        /// </summary>
        public int Count => data.Count;
        #endregion

        #region CRUD
        /// <summary>
        /// 
        /// </summary>
        public TValue Get(TKey key)
        {
            data.TryGetValue(key, out TValue value);
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        public TValue Get(Func<TValue, bool> filter)
        {
            return data.Values.FirstOrDefault(filter);
        }
        /// <summary>
        /// 
        /// </summary>
        public List<TValue> GetAll(Func<TValue, bool> filter = null)
        {
            if (filter == null)
                return data.Values.ToList();
            else
                return data.Values.Where(filter).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Add(TKey key, TValue value)
        {
            var result = data.TryAdd(key, value);
            if (result)
                ChangedAdded?.Invoke(value);
                //Task.Run(() => ChangedAdded?.Invoke(value));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Update(TKey key, TValue value)
        {
            var result = data.TryUpdate(key, value, value);
            if (result)
                ChangedUpdated?.Invoke(value);
                //Task.Run(() => ChangedUpdated?.Invoke(value));
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        public TValue Remove(TKey key)
        {
            data.TryRemove(key, out TValue value);
            if (value != null)
                ChangedRemoved?.Invoke(value);
                //Task.Run(() => ChangedRemoved?.Invoke(rtrn));
            return value;
        }
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