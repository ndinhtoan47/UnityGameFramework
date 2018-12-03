using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialize
{
    public class BinarySerialize
    {
        public static readonly BinarySerialize Instance = new BinarySerialize();

        private IFormatter formatter;

        private BinarySerialize()
        {
            formatter = new BinaryFormatter();
        }

        public void Serialize<T>(string filePath, T obj) where T : ISerializable
        {
            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, obj);
                stream.Close();
            }
        }

        public void Serialize<T>(string filePath, List<T> list) where T : ISerializable
        {
            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, list);
                stream.Close();
            }
        }

        public T Deserialize<T>(string filePath) where T : ISerializable
        {
            T result = default(T);
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                result = (T)formatter.Deserialize(stream);
                stream.Close();
            }
            return result;
        }

        public void Deserialize<T>(string filePath, ref List<T> result) where T : ISerializable
        {
            result = null;
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                result = (List<T>)formatter.Deserialize(stream);
                stream.Close();
            }
        }
    }
}