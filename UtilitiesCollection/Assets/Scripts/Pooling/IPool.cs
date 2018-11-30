
namespace Pooling.Interface
{
    public interface IPoolable
    {
        bool IsBusy { get; set; }
        void Refresh();
    }

    public interface IPool<T>
    {
        void ReturnPool(T item);
        T GetInstance();
    }
}
