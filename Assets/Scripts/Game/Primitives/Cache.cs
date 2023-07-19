using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OpenRA.Primitives
{
    public class Cache<T,U> : IReadOnlyDictionary<T,U>
    {
        private readonly Dictionary<T, U> cache;

        private readonly Func<T, U> loader;

        public Cache(Func<T, U> loader, IEqualityComparer<T> c)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            this.loader = loader;
            cache = new Dictionary<T, U>(c);
        }
        
        public Cache(Func<T,U> loader) : this(loader,EqualityComparer<T>.Default){}

        public U this[T key] => cache.GetOrAdd(key, loader);

        public IEnumerator<KeyValuePair<T, U>> GetEnumerator() { return cache.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public int Count => cache.Count;
        
        public bool ContainsKey(T key) { return cache.ContainsKey(key); }

        public bool TryGetValue(T key, out U value) { return cache.TryGetValue(key,out value); }

        public IEnumerable<T> Keys => cache.Keys;
        public IEnumerable<U> Values => cache.Values;
        
        public void Clear(){cache.Clear();}
    }
}

