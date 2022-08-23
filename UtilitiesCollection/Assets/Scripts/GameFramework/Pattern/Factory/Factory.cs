
namespace GameFramework.Pattern
{
    using GameFramework.Common;
    using System.Collections.Generic;

    public delegate TResult CreationFunc<TParam, TResult>(in TParam param);

    public class Factory<TKey, TParam, TResult> : IFactory<TKey, TParam, TResult> where TKey : System.IEquatable<TKey>, System.IDisposable
    {
        private List<WrapperByKeyValue<TKey, CreationFunc<TParam, TResult>>> _creationFuncs;

        public Factory()
        {
            _creationFuncs = new List<WrapperByKeyValue<TKey, CreationFunc<TParam, TResult>>>();
        }

        public bool Register(TKey key, CreationFunc<TParam, TResult> creationFunc)
        {
            for (int i = 0; i < _creationFuncs.Count; i++)
            {
                if (_creationFuncs[i].Key.Equals(key))
                {
                    return false;
                }
            }
            _creationFuncs.Add(new WrapperByKeyValue<TKey, CreationFunc<TParam, TResult>>(key, creationFunc));
            return true;
        }

        public bool Remove(TKey key)
        {
            for (int i = 0; i < _creationFuncs.Count; i++)
            {
                if (_creationFuncs[i].Key.Equals(key))
                {
                    _creationFuncs.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public TResult Create(TKey key, in TParam parameter)
        {
            CreationFunc<TParam, TResult> creationFunc = FindCreationFunc(key);
            return creationFunc != null ? creationFunc.Invoke(parameter) : default;
        }

        public void Dispose()
        {
            _creationFuncs.Clear();
            _creationFuncs = null;
        }

        private CreationFunc<TParam, TResult> FindCreationFunc(TKey key)
        {
            for (int i = 0; i < _creationFuncs.Count; i++)
            {
                if (_creationFuncs[i].Key.Equals(key))
                {
                    return _creationFuncs[i].Value;
                }
            }
            return null;
        }
    }
}
