﻿using GenRep.Contract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenRep.General
{
    public class ConcurrentDictionaryRepository<T> : IConcurrentDictionaryRepository<T>
            where T : class, new()
    {
        protected readonly ConcurrentDictionary<string, T> _db;
        public ConcurrentDictionaryRepository(ConcurrentDictionary<string, T> db)
        {
            _db = db;
        }

        public T TryGetValue(string key)
        {
            _db.TryGetValue(key, out var rtrn);
            return rtrn;
        }
        public List<T> GetAll(Func<T, bool> filter = null)
        {
            if (filter == null)
                return _db.Values.ToList();
            else
                return _db.Values.Where(filter).ToList();
        }
        public bool TryAdd(string key, T value)
        {
            return _db.TryAdd(key, value);
        }
        public bool TryUpdate(string key, T value)
        {
            return _db.TryUpdate(key, value, null);
        }
        public T TryRemove(string key)
        {
            _db.TryRemove(key, out var rtrn);
            return rtrn;
        }
    }
}