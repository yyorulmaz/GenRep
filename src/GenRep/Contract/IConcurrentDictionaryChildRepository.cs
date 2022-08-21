using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GenRep.Contract
{

    public interface IConcurrentDictionaryChildRepository<TKey, TValue>
        where TKey : notnull
        where TValue : notnull
    {
        #region Data
        public IConcurrentDictionaryRepository<TKey, TValue> DataMain { get; }
        public ConcurrentDictionary<TKey, TValue> Data { get; }
        #endregion

        #region Count
        public int Count { get; }
        #endregion

        #region CRUD
        TValue Get(TKey key);
        TValue Get(Func<TValue, bool> filter);
        List<TValue> GetAll(Func<TValue, bool> filter = null);
        bool Add(TKey key, TValue value);
        bool Update(TKey key, TValue value);
        TValue Remove(TKey key);
        #endregion

        #region Changed
        public event Action<TValue> ChangedAdded;
        public event Action<TValue> ChangedUpdated;
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}