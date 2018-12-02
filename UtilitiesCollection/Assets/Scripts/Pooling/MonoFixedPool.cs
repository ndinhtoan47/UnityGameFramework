using Pooling.Interface;
using Singleton.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling.Fixed
{
    public sealed class MonoFixedPool<T> : ISingleton, IPool<T> where T : Component, IPoolable
    {
        private int MaxSize;
        private List<GameObject> pool;

        public MonoFixedPool() { }

        public int HashCode
        {
            get
            {
                return this.GetHashCode();
            }
        }  

        public void Init(int capacity = 10)
        {
            if (pool != null) return;
            MaxSize = capacity;
            pool = new List<GameObject>(MaxSize);
            for (int i = 0; i < MaxSize; i++)
            {
                pool[i] = new GameObject();
                pool[i].AddComponent(typeof(T));
                pool[i].gameObject.SetActive(false);
            }
        }

        public void ReturnPool(T obj)
        {
            if (pool.Count >= MaxSize || !obj.IsBusy)
                GameObject.Destroy(obj.gameObject);
            if (obj.IsBusy)
            {
                obj.Refresh();
                obj.IsBusy = false;
                obj.gameObject.SetActive(false);
                pool.Add(obj.gameObject);
            }
        }

        public T GetInstance()
        {
            if (pool.Count > 0)
            {
                T value = pool[0].GetComponent<T>();
                value.IsBusy = true;
                pool.RemoveAt(0);
                return value;
            }
            return null;
        }
    }
}

