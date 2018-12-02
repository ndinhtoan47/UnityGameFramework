using Pooling.Interface;
using Singleton.Interface;
using System.Collections.Generic;

namespace Pooling.Dynamic
{
    /// <summary>
    ///  For GameObject has only one T Component unless may be occur exception
    /// </summary>
    /// <typeparam name="T">IPoolable component</typeparam>
    public sealed class DynamicPool<T> : ISingleton, IPrototypePool<T> where T : IPoolable, new()
    {
        private Queue<T> pool;

        public DynamicPool() { }

        public int HashCode
        {
            get { return this.GetHashCode(); }
        }

        public void Init(int capacity = 0)
        {
            if (pool != null) return;
            pool = new Queue<T>();
            for (int i = 0; i < capacity; i++)
            {
                pool.Enqueue(new T());
            }
        }

        public T GetInstance()
        {
            if (pool.Count <= 0)
                pool.Enqueue(new T());
            T value = pool.Dequeue();
            value.IsBusy = true;
            return value;
        }

        public void ReturnPool(T item)
        {
            item.IsBusy = false;
            item.Refresh();
            pool.Enqueue(item);
        }
    }
}
