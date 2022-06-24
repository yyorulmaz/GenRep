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
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; }
        /// <summary>
        /// 
        /// </summary>
        public TValue TryGet();
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentQueue<TValue> GetAll();
        /// <summary>
        /// 
        /// </summary>
        public bool TryAdd(TValue value);
        /// <summary>
        /// 
        /// </summary>
        public TValue TryRemove();

        /// <summary>
        /// 
        /// </summary>
        public event Action<bool> ChangedAdded;
        /// <summary>
        /// 
        /// </summary>
        public event Action<bool> ChangedRemoved;
    }
}
