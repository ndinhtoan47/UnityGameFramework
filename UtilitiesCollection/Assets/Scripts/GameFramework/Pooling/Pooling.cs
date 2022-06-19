namespace GameFramework.Pattern
{	
	using System;
	using System.Collections.Generic;

	public class Pooling<T> where T : IPoolable
	{
		private int _id = int.MinValue;
		private Func<T> _createMethod;
		private Queue<T> _freeObjects = new Queue<T>();
		private Dictionary<int, T> _busyObjects = new Dictionary<int, T>();

		private Pooling(Func<T> createMethod)
		{
			_createMethod = createMethod;
		}
		public T GetFreeObject()
		{
			T res = default;
			if (_freeObjects.Count != 0)
			{
				res = _freeObjects.Dequeue();
			}
			else
			{
				res = _createMethod.Invoke();
				res.SetId(_id++);
				res.SetPooling(this);
			}
			_busyObjects.Add(res.GetId(), res);
			return res;
		}
		public void GetBack(T obj)
		{
			obj.Release();
			_busyObjects.Remove(obj.GetId());
			_freeObjects.Enqueue(obj);
		}
		public void GetBackAll()
		{
			foreach (KeyValuePair<int, T> kvp in _busyObjects)
			{
				kvp.Value.Release();
				_freeObjects.Enqueue(kvp.Value);
			}
			_busyObjects.Clear();
		}
		public void ForEachActiveItems(Action<T> handler)
		{
			if (handler != null)
			{
				foreach (var it in _busyObjects)
				{
					handler.Invoke(it.Value);
				}
			}
		}
		public static Pooling<T> GetPooling(Func<T> createMethod)
		{
			return new Pooling<T>(createMethod);
		}
	}

	public class ItemWrapper<T> : IPoolable
	{
		public readonly T Obj;
		public readonly int PoolId;
		private int _id;
		private Pooling<ItemWrapper<T>> _pool;
		private static Dictionary<int, Action<T>> s_releaseFunc;

		public static void SetReleaseFunc(int poolId, Action<T> releaseFunc)
		{
			if (s_releaseFunc == null)
			{
				s_releaseFunc = new Dictionary<int, Action<T>>();
			}
			s_releaseFunc[poolId] = releaseFunc;
		}
		public ItemWrapper(int poolId, T obj)
		{
			PoolId = poolId;
			Obj = obj;
		}

		public int GetId()
		{
			return _id;
		}
		public void Release()
		{
			if (s_releaseFunc != null && s_releaseFunc.ContainsKey(PoolId))
			{
				s_releaseFunc[PoolId]?.Invoke(Obj);
			}
		}
		public void SetId(int id)
		{
			_id = id;
		}
		public void SetPooling<U>(Pooling<U> pool) where U : IPoolable
		{
			_pool = pool as Pooling<ItemWrapper<T>>;
		}
		public void GetBackPool()
		{
			_pool.GetBack(this);
		}
	}
}