using Singleton.Interface;
using System.Collections.Generic;

namespace Singleton.Generic
{
    /// <summary>
    /// For prototype class which does not inherit Unity Component
    /// </summary>
    /// <typeparam name="T">Type of singleton</typeparam>
    public sealed class Singleton<T> where T : ISingleton, new()
    {
        private static IDictionary<int, T> singletons;

        public static T Instance
        {
            get
            {
                if (singletons == null) singletons = new Dictionary<int, T>();
                int hashCode = typeof(T).GetHashCode();
                if (singletons.ContainsKey(hashCode))
                {
                    return singletons[hashCode];
                }
                T newIns = new T();
                singletons.Add(newIns.HashCode, newIns);
                return newIns;
            }
        }
    }
}
