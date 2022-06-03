using GenRep.Contract;
using System;
using System.Collections.Concurrent;

namespace GenRep.ConcurrentQueue
{
    public class ConcurrentQueueRepository<T> : IConcurrentQueueRepository<T> where T : class, new()
    {
        protected readonly ConcurrentQueue<T> _db;
        public ConcurrentQueueRepository(ConcurrentQueue<T> db)
        {
            _db = db;
        }

        public int Count => _db.Count;

        public T TryGet()
        {
            _db.TryPeek(out var data);
            return data;
        }

        public ConcurrentQueue<T> GetAll() 
        { 
            return _db; 
        }
        public bool TryAdd(T value)
        {
            try
            {
                _db.Enqueue(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T TryRemove()
        {
            _db.TryDequeue(out var data);
            return data;
        }
    }
}
