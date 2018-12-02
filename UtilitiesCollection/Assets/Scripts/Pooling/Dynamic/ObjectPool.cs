using Pooling.Interface;
using Singleton.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling.Dynamic
{
    /// <summary>
    /// For Unity Prefabs which has at least 1 T component unless may be occur exception
    /// </summary>
    /// <typeparam name="T">IPoolable component</typeparam>
    public class ObjectPool<T> : ISingleton, IPrefabPool where T : IPoolable
    {
        private Queue<GameObject> pool;

        private GameObject Origin;

        public int HashCode
        {
            get { return this.GetHashCode(); }
        }

        public ObjectPool() { }

        public GameObject GetInstance()
        {
            if (pool.Count <= 0)
            {
                GameObject newIns = GameObject.Instantiate(Origin);
                newIns.SetActive(false);
                pool.Enqueue(newIns);
            }
            return pool.Dequeue();
        }

        public void Init(GameObject prefab, int capacity = 0)
        {
            if (pool != null) return;
            else pool = new Queue<GameObject>(capacity);
            Origin = prefab;
            for (int i = 0; i < capacity; i++)
            {
                GameObject newIns = GameObject.Instantiate(Origin);
                try
                {
                    T comp = newIns.GetComponent<T>();
                    comp.IsBusy = false;
                    comp.Refresh();
                }
                catch { }

                newIns.SetActive(false);
                pool.Enqueue(newIns);
            }
        }

        public void ReturnPool(GameObject item)
        {
            try
            {
                T comp = item.GetComponent<T>();
                comp.IsBusy = false;
                comp.Refresh();
            }
            catch { }
            item.SetActive(false);
            pool.Enqueue(item);
        }
    }
}