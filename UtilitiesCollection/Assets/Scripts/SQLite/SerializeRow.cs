using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Serialize.Row
{
    [System.Serializable]
    public class SerializeRow : ISerializable
    {
        private List<object> list;

        public int Count { get { return list.Count; } }

        public object this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public SerializeRow(int capacity)
        {
            list = new List<object>(capacity);
        }

        public SerializeRow(SerializationInfo info, StreamingContext context)
        {
            list = (List<object>)info.GetValue("fieldValues", typeof(List<object>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("fieldValues", list, typeof(List<object>));
        }

        public void Add(object item)
        {
            list.Add(item);
        }

        public bool Remove(object item)
        {
            return list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public int IndexOf(object item)
        {
            return list.IndexOf(item);
        }

        public bool Contains(object item)
        {
            return list.Contains(item);
        }
    }
}