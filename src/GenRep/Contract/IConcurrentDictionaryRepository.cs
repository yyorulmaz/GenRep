using System.Collections.Generic;

namespace GenRep.Contract
{
    public interface IConcurrentDictionaryRepository<T> where T : class, new()
    {
        T TryGetValue(string key);
        List<T> GetAll();
        bool TryAdd(string key, T value);
        bool TryUpdate(string key, T value);
        T TryRemove(string key);
    }
}
