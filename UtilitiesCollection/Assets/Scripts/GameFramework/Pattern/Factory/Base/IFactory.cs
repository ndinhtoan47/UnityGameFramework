namespace GameFramework.Pattern
{
    public interface IFactory<TParameter, TResult>
    {
        TResult Create(in TParameter parameter);
    }
}