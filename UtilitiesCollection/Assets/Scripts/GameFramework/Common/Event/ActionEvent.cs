
namespace GameFramework.Pattern
{
    using System.Collections.Generic;
    using EventWrapper = GameFramework.Common.WrapperById<EventReceiver>;

    public class ActionEvent<TEventKey> : System.IDisposable
    {
        public readonly TEventKey Key;
        public readonly List<EventWrapper> Events;

        private uint _eventId;
        public ActionEvent(TEventKey key)
        {
            Key = key;
            Events = new List<EventWrapper>();
            _eventId = 0;
        }
        public void Invoke(params object[] args)
        {
            for (int i = 0; i < Events.Count; i++)
            {
                Events[i].Value.Invoke(args);
            }
        }
        public EventWrapper Subcribe(EventReceiver action)
        {
            EventWrapper evt = new EventWrapper(++_eventId, action);
            Events.Add(evt);
            return evt;
        }
        public bool Unsubcribe(EventWrapper evt)
        {
            return Unsubcribe(evt.Id);
        }
        public bool Unsubcribe(uint eventId)
        {
            for (int i = 0; i < Events.Count; i++)
            {
                EventWrapper events = Events[i];
                if (events.Id == eventId)
                {
                    Events.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public void Dispose()
        {
            Events.Clear();
        }
    }
}
