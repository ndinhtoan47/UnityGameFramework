
namespace GameFramework.Pattern
{
    public abstract class BaseMonoObserver : UnityEngine.MonoBehaviour, IObserver
    {
        private int _id;
        public void SetId(int id)
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
