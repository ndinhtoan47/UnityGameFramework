namespace GameFramework.Unity.UI
{
	using UnityEngine;
	using UnityEngine.Events;
	using UnityEngine.EventSystems;
	using System.Collections.Generic;

	class CustomEvent
	{
		public EventTriggerType TriggerType;
		public UnityEvent<BaseEventData> OnTrigger;
	}

	[RequireComponent(typeof(EventTrigger))]
	public class ImageButton : MonoBehaviour
	{

		[SerializeField] private EventTrigger _triggers;

		public bool Interactable { get; set; } = true;

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

			for (int i = 0; i < _events.Count; i++)
			{
				if(_events[i].TriggerType == type)
				{
					_events[i].OnTrigger.Invoke(eventData);
					break;
				}
			}
		}

		private void Reset()
		{
			_triggers = GetComponent<EventTrigger>();
		}
	}
}