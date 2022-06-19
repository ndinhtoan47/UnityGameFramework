namespace GameFramework.Pattern
{
    using UnityEngine;
	using System.Collections.Generic;
	using Pooling = Pooling<ItemWrapper<UnityEngine.GameObject>>;

	[System.Serializable]
	class PoolRef
	{
		public int poolId;
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
                        _poolRefs[i].poolId = ++autoPoolId;
                    }

                    if (_poolRefs[i].poolId > 0)
                    {
                        PoolRef captureRef = _poolRefs[i];
                        int poolId = captureRef.poolId;
                        Pooling pool = Pooling.GetPooling(() =>
                        {
                            return new ItemWrapper<GameObject>(poolId, Instantiate(captureRef.template, captureRef.parent));
                        });
                        ItemWrapper<GameObject>.SetReleaseFunc(poolId, (obj) =>
                        {
                            obj.transform.SetParent(captureRef.parent);
                            obj.SetActive(false);
                        });
                        _pools.Add(poolId, pool);
                    }
                    else
                    {
                        throw new System.Exception("Pood Id should greater than zero.");
                    }
                }
                IsInited = true;
            }
        }

        public Pooling GetPool(int poolId)
        {
            Init();
            if (_pools == null || !_pools.ContainsKey(poolId))
            {
                return null;
            }
            return _pools[poolId];
        }
    }
}

