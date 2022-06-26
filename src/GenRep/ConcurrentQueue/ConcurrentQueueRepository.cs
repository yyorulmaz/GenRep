using GenRep.Contract;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace GenRep.ConcurrentQueue
{
    public class ConcurrentQueueRepository<TValue> : IConcurrentQueueRepository<TValue>
    //where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ConcurrentQueue<TValue> _db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public ConcurrentQueueRepository(ConcurrentQueue<TValue> db)
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
        public ConcurrentQueue<TValue> Data => _db;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TValue TryGet()
        {
            _db.TryPeek(out var data);
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ConcurrentQueue<TValue> GetAll()
        {
            return _db;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAdd(TValue value)
        {
            try
            {
                _db.Enqueue(value);
                Task.Run(() => ChangedAdded?.Invoke(true));
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
        public TValue TryRemove()
        {
            var result = _db.TryDequeue(out var data);
            if (result)
                Task.Run(() => ChangedRemoved?.Invoke(true));
            return data;
        }

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
