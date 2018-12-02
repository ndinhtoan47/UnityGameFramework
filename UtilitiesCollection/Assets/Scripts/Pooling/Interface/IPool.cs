
using UnityEngine;

namespace Pooling.Interface
{
    public interface IPoolable
    {
        bool IsBusy { get; set; }
        void Refresh();
        void ReturnPool();
    }

    public interface IPrototypePool<T> where T : IPoolable
    {
        void Init(int capacity);
        void ReturnPool(T item);
        T GetInstance();
    }

    public interface IPrefabPool
    {
        void Init(GameObject prefab, int capacity);
        void ReturnPool(GameObject item);
        GameObject GetInstance();
    }
}
