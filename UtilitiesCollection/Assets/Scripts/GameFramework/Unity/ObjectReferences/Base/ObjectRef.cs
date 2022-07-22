namespace GameFramework.Unity.ObjectReferences
{
	public class ObjectRef<T> : UnityEngine.ScriptableObject
	{
		[UnityEngine.SerializeField] protected T[] _objs;

		public T this[int index]
		{
			get	
			{ 
				if(index >= 0 && index < _objs.Length)
				{
					return _objs[index];
				}
				return default;
			}
		}
		
		public int Count 
		{
			get 
			{ 
				if(_objs != null)
				{
					return _objs.Length;
				}
				return 0;
			}
		}
	
		public virtual int FindIndex(System.Func<T, bool> predict)
		{
			if (predict != null && _objs != null)
			{
				for (int i = 0; i < _objs.Length; i++)
				{
					if (predict.Invoke(_objs[i]))
					{
						return i;
					}
				}
			}
			return -1;
		}
	}
}