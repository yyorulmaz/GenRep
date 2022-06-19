﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenRep.Contract
{
    public interface IConcurrentDictionaryRepository<T> 
        //where T : class, new()
        where T : notnull
    {
        public int Count { get; }
        T TryGetValue(string key);
        T TryGetValue(Func<T, bool> filter);
        List<T> GetAll(Func<T, bool> filter = null);
        bool TryAdd(string key, T value);
        bool TryUpdate(string key, T value);
        T TryRemove(string key);
    }
}