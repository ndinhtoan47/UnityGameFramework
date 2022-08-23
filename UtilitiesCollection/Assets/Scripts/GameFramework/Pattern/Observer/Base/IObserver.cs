namespace GameFramework.Pattern
{
    public interface IObserver : System.IEquatable<IObserver>
    {
        int GetId();
        void OnNotify(ISubject subject);
    }
}


