using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Lab5Games.Events
{
    [System.Serializable]
    public class TypeEvent : UnityEvent<object> { }

    public static class EventManager
    {
        static Dictionary<int, TypeEvent> _eventTable = new Dictionary<int, TypeEvent>();

        public static void RegisterObserver(int event_id, UnityAction<object> listener)
        {
            TypeEvent thisEvent = null;
            if(_eventTable.TryGetValue(event_id, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new TypeEvent();
                thisEvent.AddListener(listener);

                _eventTable.Add(event_id, thisEvent);
            }
        }

        public static void UnregisterObserver(int event_id, UnityAction<object> listener)
        {
            TypeEvent thisEvent = null;
            if(_eventTable.TryGetValue(event_id, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void Clear(int event_id)
        {
            _eventTable.Remove(event_id);
        }

        public static void Clear()
        {
            _eventTable.Clear();
        }
    }
}
