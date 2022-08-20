namespace GameFramework.Pattern
{
    using System.Collections.Generic;

    using EventWrapper = GameFramework.Common.WrapperById<EventReceiver>;
    
    public delegate void EventReceiver(params object[] args);


    public sealed class EventDispatcher
    {
        private EventWrapper DefaultWrapper = new EventWrapper(uint.MinValue, null); 
        
        private uint _eventWrapperId = uint.MinValue;
        
        private Dictionary<string, List<EventWrapper>> _events = new Dictionary<string, List<EventWrapper>>();

        public EventWrapper Subcribe(string eventKey, EventReceiver receiver)
        {
            if (receiver == null)
            {
                return DefaultWrapper;
            }

            _eventWrapperId++;            
            EventWrapper wrapper = new EventWrapper(_eventWrapperId, receiver);

            if (_events.ContainsKey(eventKey))
            {
                _events[eventKey].Add(wrapper);
            }
            else
            {
                _events.Add(eventKey, new List<EventWrapper>() { wrapper });
            }

            return wrapper;
        }

        public bool Unsubcribe(string eventKey, EventWrapper wrapper)
        {
            return InternalUnsubscribe(eventKey, wrapper.Id);
        }

        public bool Dispatch(string eventKey, params object[] args)
        {
            if (_events.ContainsKey(eventKey))
            {
                List<EventWrapper> listeners = _events[eventKey];
                if (listeners != null)
                {
                    for (int i = 0; i < listeners.Count; i++)
                    {
                        listeners[i].Value.Invoke(args);
                    }
                }
                return true;
            }
            return false;
        }
        
        private bool InternalUnsubscribe(string eventKey, uint id)
        {
            // The id never equal min value
            if (id == uint.MinValue)
            {
                return false;
            }

            if (_events.ContainsKey(eventKey))
            {
                int removeIndex = _events[eventKey].FindIndex(evt => evt.Id == id);
                if (removeIndex >= 0)
                {
                    _events[eventKey].RemoveAt(removeIndex);
                    return true;
                }
            }
            return false;
        }
    }
}

