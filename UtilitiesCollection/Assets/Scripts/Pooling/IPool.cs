
namespace Pooling.Interface
{
    public interface IPoolable
    {
        bool IsBusy { get; set; }
        void Refresh();
        void ReturnPool();
    }

    public interface IPool<T> where T : IPoolable
    {
        void Init(int capacity);
        void ReturnPool(T item);
        T GetInstance();
    }
}
