
namespace Singleton.Interface
{
    public interface ISingleton<T> where T : class
    {
        T Instance { get; }
    }
}
