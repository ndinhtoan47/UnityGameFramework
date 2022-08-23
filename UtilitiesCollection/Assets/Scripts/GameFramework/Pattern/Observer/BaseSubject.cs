
namespace GameFramework.Pattern
{
    using System.Collections.Generic;

    public class BaseSubject<TObserver> : ISubject, System.IDisposable
        where TObserver : IObserver
    {
        private List<TObserver> _observers;

        public BaseSubject()
        {
            _observers = new List<TObserver>();
        }

        public void Clear()
        {
            _observers.Clear();
        }

        public void Dispose()
        {
            Clear();
            _observers = null;
        }

        public void Notify()
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].OnNotify(this);
            }
        }

        public bool Subcribe(IObserver observer)
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                if (_observers[i].Equals(observer))
                {
                    return false;
                }
            }
            _observers.Add((TObserver)observer);
            return true;
        }

        public bool Unsubcribe(IObserver observer)
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                if (_observers[i].Equals(observer))
                {
                    _observers.RemoveAt(i);
                    return true;
                }
            } 
            return false;
        }
    }
}
