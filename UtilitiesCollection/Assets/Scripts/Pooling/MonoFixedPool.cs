using Pooling.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoFixedPool<T> : IPool<T> where T : Component , IPoolable 
{
    private readonly int MaxSize;
    private readonly Component[] pool;

    private int index;

    public MonoFixedPool(int argMaxSize)
    {
        pool = new T[argMaxSize];
        for (int i = 0; i < argMaxSize; i++)
        {
            pool[i] = new GameObject().AddComponent(typeof(T));
            pool[i].gameObject.SetActive(false);
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
            obj.gameObject.SetActive(false);
        }
    }

    public T GetInstance()
    {
        if (index < MaxSize)
        {
            pool[index].IsBusy = true;
            return pool[index++];
        }
        return default(T);
    }
}

