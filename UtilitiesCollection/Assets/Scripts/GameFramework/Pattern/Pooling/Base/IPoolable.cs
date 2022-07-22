namespace GameFramework.Pattern
{    
    public interface IPoolable
    {
        int GetId();
        void SetId(int id);
        void Release();    
        void SetPooling<T>(Pooling<T> pool) where T : IPoolable;
    }
}