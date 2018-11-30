using Pooling.Interface;
using UnityEngine;

namespace Pooling
{
    public sealed class FixedPool<T> : IPool<T> where T : IPoolable, new()
    {
        private readonly int MaxSize;
        private readonly T[] pool;

        private int index;
        
        public FixedPool(int argMaxSize)
        {
            pool = new T[argMaxSize];
            for (int i = 0; i < argMaxSize; i++)
            {
                pool[i] = new T();
            }
            MaxSize = argMaxSize;
            index = 0;
        }

        public void ReturnPool(T obj)
        {
            if (obj.IsBusy)
            {
                obj.Refresh();
                index = (index - 1 < 0 ? 0 : index - 1);
                obj.IsBusy = false;
            }
        }

        public T GetInstance()
        {
            if(index < MaxSize)
            {
                pool[index].IsBusy = true;
                return pool[index++];
            }
            return default(T);
        }
    }
}
