using GenRep.Contract;
using System;
using System.Collections.Concurrent;

namespace GenRep.ConcurrentQueue
{
    public class ConcurrentQueueRepository<T> : IConcurrentQueueRepository<T> 
        //where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ConcurrentQueue<T> _db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public ConcurrentQueueRepository(ConcurrentQueue<T> db)
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
        /// <returns></returns>
        public T TryGet()
        {
            _db.TryPeek(out var data);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ConcurrentQueue<T> GetAll() 
        { 
            return _db; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAdd(T value)
        {
            try
            {
                _db.Enqueue(value);
                ChangedAdded?.Invoke(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T TryRemove()
        {
            _db.TryDequeue(out var data);
            ChangedDeleted?.Invoke(true);
            return data;
        }

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
