using GenRep.Contract;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace GenRep.ConcurrentQueue
{
    public class ConcurrentQueueRepository<TValue> : IConcurrentQueueRepository<TValue>
    //where T : class, new()
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public ConcurrentQueueRepository(ConcurrentQueue<TValue> data)
        {
            this.data = data;
        }
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentQueueRepository()
        {
            this.data = new ConcurrentQueue<TValue>();
        }
        #endregion

        #region Data
        /// <summary>
        /// 
        /// </summary>
        private readonly ConcurrentQueue<TValue> data;
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentQueue<TValue> Data => data;
        #endregion

        #region Count
        /// <summary>
        /// 
        /// </summary>
        public int Count => data.Count;
        #endregion

        #region CRUD
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TValue Get()
        {
            data.TryPeek(out var value);
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ConcurrentQueue<TValue> GetAll()
        {
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(TValue value)
        {
            try
            {
                data.Enqueue(value);
                ChangedAdded?.Invoke(value);
                //Task.Run(() => ChangedAdded?.Invoke(value));
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
