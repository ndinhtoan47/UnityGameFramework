using Pooling.Interface;
using Singleton.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling.Fixed
{
    /// <summary>
    /// For prototype class which does not inherit Unity Component
    /// </summary>
    /// <typeparam name="T">Type of class</typeparam>
    public sealed class FixedPool<T> : ISingleton, IPrototypePool<T> where T : IPoolable, new()
    {
        private int MaxSize;
        private List<T> pool;
        
        public int HashCode
        {
            get { return this.GetHashCode(); }
        }

        public FixedPool() { }

        public void Init(int capacity = 10)
        {
            if (pool != null) return;
            MaxSize = capacity;
            pool = new List<T>(MaxSize);
            for (int i = 0; i < MaxSize; i++)
            {
                pool[i] = new T();
                pool[i].Refresh();
            }
        }

        public void ReturnPool(T obj)
        {
            if (pool.Count >= MaxSize || !obj.IsBusy)
                return;
            if (obj.IsBusy)
            {
                obj.Refresh();
                obj.IsBusy = false;
                pool.Add(obj);
            }
        }

        public T GetInstance()
        {
            if(pool.Count > 0)
            {
                pool[0].IsBusy = true;
                pool.RemoveAt(0);
                return pool[0];
            }
            return default(T);
        }
    }
}
