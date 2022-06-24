namespace GameFramework.Pattern
{
    using System.Collections.Generic;
	using UnityEngine;
	using Pooling = Pooling<ItemWrapper<UnityEngine.GameObject>>;

	[System.Serializable]
	class PoolRef
	{
		public int internalPoolId;
		public int uniquePoolId;
		public Transform parent;
		public GameObject template;
	}

	public class PoolingForGameObject : MonoBehaviour
	{
		[SerializeField] private bool _isAutoPoolId = false;
		[SerializeField] private List<PoolRef> _poolRefs = null;
		public bool IsInited { get; private set; } = false;
		private Dictionary<int, Pooling> _pools = new Dictionary<int, Pooling>();

		protected virtual void Awake()
		{
			Init();
		}

		public virtual void Init()
		{
			if (!IsInited)
			{
				int autoPoolId = 0;
				for (int i = 0; i < _poolRefs.Count; i++)
				{
					if (_isAutoPoolId)
					{
						_poolRefs[i].internalPoolId = ++autoPoolId;
					}

					if (_poolRefs[i].internalPoolId > 0)
					{
						PoolRef captureRef = _poolRefs[i];

						int uniquePoolId = ItemWrapper<GameObject>.NewPoolId();
						_poolRefs[i].uniquePoolId = uniquePoolId;

						Pooling pool = Pooling.GetPooling(() =>
						{
							return new ItemWrapper<GameObject>(uniquePoolId, Instantiate(captureRef.template, captureRef.parent));
						});
						ItemWrapper<GameObject>.SetReleaseFunc(uniquePoolId, (obj) =>
						{
							obj.transform.SetParent(captureRef.parent);
							obj.SetActive(false);
						});
						_pools.Add(_poolRefs[i].internalPoolId, pool);
					}
					else
					{
						throw new System.Exception("Pood Id should greater than zero.");
					}
				}
				IsInited = true;
			}
		}

		public Pooling GetPool(int internalPoolId)
		{
			Init();
			if (_pools == null || !_pools.ContainsKey(internalPoolId))
			{
				return null;
			}
			return _pools[internalPoolId];
		}

		public void SetPoolingReleseFunc(int internalPoolId, System.Action<GameObject> releaseFunc)
		{
			Init();
			if (_pools == null || !_pools.ContainsKey(internalPoolId))
			{
				return;
			}
			for (int i = 0; i < _poolRefs.Count; i++)
			{
				if (_poolRefs[i].internalPoolId == internalPoolId)
				{
					ItemWrapper<GameObject>.SetReleaseFunc(uniquePoolId: _poolRefs[i].uniquePoolId, releaseFunc: releaseFunc);
					break;
				}
			}	
		}
	}
}

