namespace GameFramework.Pattern
{
    /// <summary>
    /// The Subject interface is used in Observer Pattern
    /// </summary>
    public interface ISubject
    {
        bool Subcribe(IObserver observer);
        bool Unsubcribe(IObserver observer);
        void Notify();
        void Clear();
    }
}