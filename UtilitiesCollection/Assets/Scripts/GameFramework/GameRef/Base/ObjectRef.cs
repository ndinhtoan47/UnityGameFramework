namespace GameFramework.Storage
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
	}
}