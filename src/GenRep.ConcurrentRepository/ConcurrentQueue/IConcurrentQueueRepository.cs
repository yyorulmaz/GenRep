using System;
using System.Collections.Concurrent;

namespace GenRep.ConcurrentRepository.ConcurrentQueue
{
    public interface IConcurrentQueueRepository<TValue> 
    {
        #region Data
        ConcurrentQueue<TValue> Data { get; }
        #endregion

        #region Count
        int Count { get; }
        #endregion

        #region CRUD
        TValue Get();
        ConcurrentQueue<TValue> GetAll();
        bool Add(TValue value);
        TValue Remove();
        #endregion

        #region Changed
        event Action<TValue> ChangedAdded;
        event Action<TValue> ChangedRemoved;
        #endregion
    }
}
