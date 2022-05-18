namespace GameFramework.Pattern
{
    public interface IObserver
    {
        int GetId();
        void OnNotify(ISubject subject);
    }
}


