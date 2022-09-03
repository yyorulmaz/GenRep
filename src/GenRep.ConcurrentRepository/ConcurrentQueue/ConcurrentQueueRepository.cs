using System;
using System.Collections.Concurrent;

namespace GenRep.ConcurrentRepository.ConcurrentQueue
{
    public class ConcurrentQueueRepository<TValue> : IConcurrentQueueRepository<TValue>
    {
        #region Constructor
        public ConcurrentQueueRepository(ConcurrentQueue<TValue> data)
        {
            this.data = data;
        }
        public ConcurrentQueueRepository()
        {
            this.data = new ConcurrentQueue<TValue>();
        }
        #endregion

        #region Data
        private readonly ConcurrentQueue<TValue> data;
        public ConcurrentQueue<TValue> Data => data;
        #endregion

        #region Count
        public int Count => data.Count;
        #endregion

        #region CRUD
        public TValue Get()
        {
            data.TryPeek(out var value);
            return value;
        }
        public ConcurrentQueue<TValue> GetAll()
        {
            return data;
        }
        public bool Add(TValue value)
        {
            try
            {
                data.Enqueue(value);
                ChangedAdded?.Invoke(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public TValue Remove()
        {
            var result = data.TryDequeue(out var value);
            if (result)
                ChangedRemoved?.Invoke(value);
                //Task.Run(() => ChangedRemoved?.Invoke(value));
            return value;
        }
        #endregion

        #region Changed
        public event Action<TValue> ChangedAdded;
        public event Action<TValue> ChangedRemoved;
        #endregion
    }
}
