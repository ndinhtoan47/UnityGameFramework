using Pooling.Interface;
using System.Collections.Generic;

public class DynamicPool<T> : IPool<T> where T : IPoolable, new()
{
    private Queue<T> queue;

    public DynamicPool()
    {
        queue = new Queue<T>();
    }

    public T GetInstance()
    {
        if (queue.Count <= 0)
            queue.Enqueue(new T());
        return queue.Dequeue();
    }

    public void ReturnPool(T item)
    {
        item.IsBusy = false;
        item.Refresh();
        queue.Enqueue(item);
    }
}
