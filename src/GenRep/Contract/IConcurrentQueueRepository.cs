using System;
using System.Collections.Concurrent;

namespace GenRep.Contract
{
    public interface IConcurrentQueueRepository<TValue> 
    {
        #region Data
        public ConcurrentQueue<TValue> Data { get; }
        #endregion

        #region Count
        public int Count { get; }
        #endregion

        #region CRUD
        public TValue Get();
        public ConcurrentQueue<TValue> GetAll();
        public bool Add(TValue value);
        public TValue Remove();
        #endregion

        #region Changed
        public event Action<TValue> ChangedAdded;
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}
