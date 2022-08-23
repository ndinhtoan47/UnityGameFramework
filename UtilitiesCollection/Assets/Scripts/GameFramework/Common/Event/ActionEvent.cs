
namespace GameFramework.Pattern
{
    using System.Collections.Generic;
    using EventWrapper = GameFramework.Common.WrapperById<EventReceiver>;

    public class ActionEvent<TEventKey> : System.IDisposable
    {
        public readonly TEventKey Key;
        protected List<EventWrapper> _events;

        private uint _eventId;
        public ActionEvent(TEventKey key)
        {
            Key = key;
            _events = new List<EventWrapper>();
            _eventId = 0;
        }
        public void Invoke(params object[] args)
        {
            for (int i = 0; i < _events.Count; i++)
            {
                _events[i].Value.Invoke(args);
            }
        }
        public EventWrapper Subcribe(EventReceiver action)
        {
            EventWrapper evt = new EventWrapper(++_eventId, action);
            _events.Add(evt);
            return evt;
        }
        public bool Unsubcribe(EventWrapper evt)
        {
            return Unsubcribe(evt.Id);
        }
        public bool Unsubcribe(uint eventId)
        {
            for (int i = 0; i < _events.Count; i++)
            {
                EventWrapper events = _events[i];
                if (events.Id == eventId)
                {
                    _events.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public void Dispose()
        {
            _events.Clear();
            _events = null;
        }
    }
}
