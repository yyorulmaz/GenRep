using System.Collections.Concurrent;

namespace GenRep.Contract
{
    public interface IConcurrentQueueRepository<T> 
        //where T : class, new()
    {
        public int Count { get; }
        public T TryGet();
        public ConcurrentQueue<T> GetAll();
        public bool TryAdd(T value);
        public T TryRemove();
    }
}
