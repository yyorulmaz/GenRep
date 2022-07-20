using System;
using System.Collections.Concurrent;

namespace GenRep.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConcurrentQueueRepository<TValue> 
        //where T : class, new()
    {
        #region Data
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentQueue<TValue> Data { get; }
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
        public TValue Get();
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentQueue<TValue> GetAll();
        /// <summary>
        /// 
        /// </summary>
        public bool Add(TValue value);
        /// <summary>
        /// 
        /// </summary>
        public TValue Remove();
        #endregion

        #region Changed
        /// <summary>
        /// 
        /// </summary>
        public event Action<TValue> ChangedAdded;
        /// <summary>
        /// 
        /// </summary>
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}
