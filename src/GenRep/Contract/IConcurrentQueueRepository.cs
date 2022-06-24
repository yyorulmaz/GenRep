using System;
using System.Collections.Concurrent;

namespace GenRep.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConcurrentQueueRepository<T> 
        //where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; }
        /// <summary>
        /// 
        /// </summary>
        public T TryGet();
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentQueue<T> GetAll();
        /// <summary>
        /// 
        /// </summary>
        public bool TryAdd(T value);
        /// <summary>
        /// 
        /// </summary>
        public T TryRemove();

        /// <summary>
        /// 
        /// </summary>
        public event Action<bool> ChangedAdded;
        /// <summary>
        /// 
        /// </summary>
        public event Action<bool> ChangedDeleted;
    }
}
