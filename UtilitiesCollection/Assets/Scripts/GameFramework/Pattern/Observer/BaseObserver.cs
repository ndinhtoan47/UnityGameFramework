
namespace GameFramework.Pattern
{
    public abstract class BaseObserver : IObserver
    {
        private readonly int _id;
        public BaseObserver(int id)
        {
            _id = id;
        }

        public int GetId()
        {
            return _id;
        }
        public bool Equals(IObserver other)
        {
            return other.GetId() == this.GetId();
        }
        public abstract void OnNotify(ISubject subject);
    }
}
