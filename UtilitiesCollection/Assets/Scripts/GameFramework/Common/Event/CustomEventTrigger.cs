
namespace GameFramework.Common
{
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using System.Collections.Generic;

    public class CustomEvent
    {
        public EventTriggerType TriggerType;
        public UnityEvent<BaseEventData> OnTrigger;
    }

	[UnityEngine.RequireComponent(typeof(EventTrigger))]
	public class CustomEventTrigger : UnityEngine.MonoBehaviour
    {
		public bool Interactable = true;

		[UnityEngine.SerializeField] private EventTrigger _triggers;

		private List<CustomEvent> _events = new List<CustomEvent>();

		public UnityEvent<BaseEventData> GetEvent(EventTriggerType type)
		{
			int index = _events.FindIndex(e => e.TriggerType == type);

			if (index >= 0)
			{
				return _events[index].OnTrigger;
			}
			else
			{
				return RegisterEvent(type);
			}
		}

		public bool Invoke(EventTriggerType type, BaseEventData eventData)
        {
			for (int i = 0; i < _events.Count; i++)
			{
				if (_events[i].TriggerType == type)
				{
					_events[i].OnTrigger.Invoke(eventData);
					return true;
				}
			}
			return false;
        }

		private UnityEvent<BaseEventData> RegisterEvent(EventTriggerType type)
		{
			EventTrigger.Entry onClickEntry = new EventTrigger.Entry
			{
				eventID = type
			};
			onClickEntry.callback.AddListener((eventData) => OnEvent(type, eventData));
			_triggers.triggers.Add(onClickEntry);

			CustomEvent e = new CustomEvent
			{
				TriggerType = type,
				OnTrigger = new UnityEvent<BaseEventData>()
			};
			_events.Add(e);

			return e.OnTrigger;
		}

		private void OnEvent(EventTriggerType type, BaseEventData eventData)
		{
			if (!Interactable) { return; }

			Invoke(type, eventData);
		}

		protected virtual void Reset()
		{
			_triggers = GetComponent<EventTrigger>();
		}
	}

}
