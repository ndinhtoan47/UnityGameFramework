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

       public void StringSerialize(string filePath, string value)
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, value);
                    stream.Close();
                }
            }
            catch { }
        }

        public void PrimarySerialize<T>(string filePath, T value)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, value);
                    stream.Close();
                }
            }
            catch { }
        }

        public void PrimarySerialize<T, Collection>(string filePath, Collection value)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
            where Collection : ICollection<T>
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, value);
                    stream.Close();
                }
            }
            catch { }
        }

        public string StringDeserialize(string filePath)
        {
            string result = null;
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    result = (string)formatter.Deserialize(stream);
                    stream.Close();
                }

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
            return result;
        }

        public T PrimaryDeserialize<T>(string filePath)
            where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
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
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
            return result;
        }

        public Collection PrimaryDeserialize<T, Collection>(string filePath)
           where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
           where Collection : ICollection<T>
        {
            Collection result = default(Collection);
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    result = (Collection)formatter.Deserialize(stream);
                    stream.Close();
                }
            }
            catch { }
            return result;
        }

        public void CustomSerialize<T>(string filePath, T value) where T : ISerializable
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, value);
                    stream.Close();
                }
            }
            catch { }
        }
          
        public void CustomSerialize<T, Collection>(string filePath, Collection value)
           where T : ISerializable
           where Collection : ICollection<T>
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, value);
                    stream.Close();
                }
            }
            catch { }
        }

        public T CustomDeserialize<T>(string filePath)
           where T : ISerializable
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