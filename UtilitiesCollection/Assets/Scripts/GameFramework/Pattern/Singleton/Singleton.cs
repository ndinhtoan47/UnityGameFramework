namespace GameFramework.Pattern
{
    /// <summary>
    /// Class <c>T</c> doesn't need to be a MonoBehaviour
    /// </summary>
    public abstract class Singleton<T> where T : new()
    {
        private static T _instance;
        private static object _locker = new object();
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                            _instance = new T();
                    }
                }
                return _instance;
            }
        }
    }

    /// <summary>
    /// Class <c>T</c> need to be a MonoBehaviour
    /// </summary>
    public class MonoSingleton<T> : UnityEngine.MonoBehaviour where T : UnityEngine.Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    System.Type insType = typeof(T);
                    _instance = UnityEngine.GameObject.FindObjectOfType(insType) as T;
                    if (_instance == null)
                    {
                        string insName = $"Singleton [{insType}]";

                        UnityEngine.GameObject obj = new UnityEngine.GameObject(insName, insType);                    
                        
                        obj.hideFlags = UnityEngine.HideFlags.DontSave;
                        
                        _instance = obj.GetComponent<T>();
                        
                        MonoSingleton<T> monoIns = _instance as MonoSingleton<T>;
                        
                        monoIns.InternalInit();
                    }
                }
                return _instance;
            }
        }

        protected virtual void InternalInit() { }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != (this as T))
            {
                Destroy(gameObject);
            }
        }

        public static void ResetSingleton()
        {
            if (_instance != null)
            {
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }
    }
}