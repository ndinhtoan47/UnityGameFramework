namespace GameFramework.Pattern
{
	using UnityEngine;
	public class PoolingForGameObject : MonoBehaviour
	{
		private static int S_POOL_ID = 0;

		[SerializeField] Transform _container;
		[SerializeField] GameObject _template;

		public int PoolId { get; private set; }
		public bool IsInited { get; private set; } = false;
		public Pooling<ItemWrapper<GameObject>> Pool { get; private set; }

		protected virtual void Awake()
		{
			Init();
		}

		public virtual void Init()
		{
			if (!IsInited)
			{
				PoolId = S_POOL_ID++;
				Pool = Pooling<ItemWrapper<GameObject>>.GetPooling(() =>
				{
					return new ItemWrapper<GameObject>(PoolId, Instantiate(_template, _container));
				});
				ItemWrapper<GameObject>.SetReleaseFunc(PoolId, (obj) =>
				{
					obj.transform.SetParent(_container);
					obj.SetActive(false);
				});
				IsInited = true;
			}
		}
	}
}

