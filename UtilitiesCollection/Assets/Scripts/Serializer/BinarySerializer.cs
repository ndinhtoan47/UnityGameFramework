using Singleton.Interface;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializer
{
    public sealed class BinarySerializer : ISingleton
    {
        private IFormatter formatter;

        public int HashCode
        {
            get { return this.GetHashCode(); }
        }

        public BinarySerializer()
        {
            formatter = new BinaryFormatter();
        }

        public void Serialize(string filePath, FileMode mode, object ins, bool clearExistFile = false)
        {
            try
            {
                using (Stream stream = new FileStream(filePath, mode, FileAccess.Write, FileShare.None))
                {
                    if (clearExistFile)
                        stream.Flush();
                    formatter.Serialize(stream, ins);
                    stream.Close();
                }
            }
            catch { }
        }

        public void Serialize<T>(string filePath, FileMode mode, T ins, bool clearExistFile = false) where T : ISerializable
        {
            try
            {
                using (Stream stream = new FileStream(filePath, mode, FileAccess.Write, FileShare.None))
                {
                    if (clearExistFile)
                        stream.Flush();
                    formatter.Serialize(stream, ins);
                    stream.Close();
                }
            }
            catch { }
        }

        public object Deserialize(string filePath)
        {
            object result = null;
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    result = formatter.Deserialize(stream);
                    stream.Close();
                }
            }
            catch { }
            return result;
        }

        public T Deserialize<T>(string filePath) where T : ISerializable
        {
            T result = default(T);
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    result = (T)formatter.Deserialize(stream);
                    stream.Close();
                }
            }
            catch { }
            return result;
        }

        public ICollection<object> CollectionDeserialize(string filePath)
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    ICollection<object> result = null;
                    result = (ICollection<object>)formatter.Deserialize(stream);
                    stream.Close();
                    return result;
                }
            }
            catch { return null; }
        }

        public ICollection<T> CollectionDeserialize<T>(string filePath) where T : ISerializable
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    ICollection<T> result = null;
                    result = (ICollection<T>)formatter.Deserialize(stream);
                    stream.Close();
                    return result;
                }
            }
            catch { return null; }
        }
    }
}