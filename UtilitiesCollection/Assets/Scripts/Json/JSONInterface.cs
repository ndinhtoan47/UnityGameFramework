
namespace Json.Interface
{
    interface IJSON<T>
    {
        T FromJSON();
        string ToJSON();
    }
}