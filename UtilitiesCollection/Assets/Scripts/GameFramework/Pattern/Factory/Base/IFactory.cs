namespace GameFramework.Pattern
{
    public interface IFactory<TKey, TParameter, TResult> where TKey : System.IEquatable<TKey>
    {
        TResult Create(TKey key, in TParameter parameter);
    }
}