﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GenRep.ConcurrentRepository.ConcurrentDictionary
{
    public class ConcurrentDictionaryChildRepository<TKey, TValue> : IConcurrentDictionaryChildRepository<TKey, TValue>
        //where TKey : notnull
        //where TValue : notnull
    {
        #region Constructor
        public ConcurrentDictionaryChildRepository(IConcurrentDictionaryRepository<TKey, TValue> dataMain, ConcurrentDictionary<TKey, TValue> data)
        {
            this.dataMain = dataMain;
            this.data = data;
        }
        public ConcurrentDictionaryChildRepository(IConcurrentDictionaryRepository<TKey, TValue> dataMain)
        {
            this.dataMain = dataMain;
            this.data = new ConcurrentDictionary<TKey, TValue>();
        }
        #endregion

        #region Data
        private readonly IConcurrentDictionaryRepository<TKey, TValue> dataMain;
        public IConcurrentDictionaryRepository<TKey, TValue> DataMain => dataMain;


        private readonly ConcurrentDictionary<TKey, TValue> data;
        public ConcurrentDictionary<TKey, TValue> Data => data;
        #endregion

        #region Count
        public int Count => data.Count;
        #endregion

        #region CRUD
        public TValue Get(TKey key)
        {
            data.TryGetValue(key, out TValue value);
            return value;
        }
        public TValue Get(Func<TValue, bool> filter)
        {
            return data.Values.FirstOrDefault(filter);
        }
        public List<TValue> GetAll(Func<TValue, bool> filter = null)
        {
            if (filter == null)
                return data.Values.ToList();
            else
                return data.Values.Where(filter).ToList();
        }
        public bool Add(TKey key, TValue value)
        {
            dataMain.Add(key, value);
            var result = data.TryAdd(key, value);
            if (result)
                ChangedAdded?.Invoke(value);
            return result;
        }
        public bool Update(TKey key, TValue value)
        {
            dataMain.Update(key, value);
            var result = data.TryUpdate(key, value, value);
            if (result)
                ChangedUpdated?.Invoke(value);
            return result;
        }
        public TValue Remove(TKey key)
        {
            dataMain.Remove(key);
            data.TryRemove(key, out TValue value);
            if (value != null)
                ChangedRemoved?.Invoke(value);
            return value;
        }
        #endregion

        #region Changed
        public event Action<TValue> ChangedAdded;
        public event Action<TValue> ChangedUpdated;
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}