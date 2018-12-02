using Singleton.Interface;
using UnityEngine;

namespace Singleton.Mono
{
    public sealed class MonoSingleton<T> : ISingleton where T : Component
    {
        public static T Instance
        {
            get
            {
                T[] instances = GameObject.FindObjectsOfType(typeof(T)) as T[];
                if (instances.Length > 0)
                {
                    if (instances.Length > 1)
                    {
                        for (int i = 1; i < instances.Length; i++)
                        {
                            GameObject.Destroy(instances[i].gameObject);
                        }
                    }
                    return instances[0];
                }
                else
                {
                    GameObject obj = new GameObject()
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    return obj.AddComponent<T>();
                }

            }
        }

        public int HashCode
        {
            get { return this.GetHashCode(); }
        }
    }
}