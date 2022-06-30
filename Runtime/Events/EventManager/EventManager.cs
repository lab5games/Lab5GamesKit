using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Lab5Games.Events
{
    [System.Serializable]
    public class TypeEvent : UnityEvent<object> { }

    public static class EventManager
    {
        static Dictionary<int, TypeEvent> _eventTables = new Dictionary<int, TypeEvent>();
        
        public static void RegisterListener(int event_id, UnityAction<object> listener)
        {
            TypeEvent thisEvent = null;
            if(_eventTables.TryGetValue(event_id, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new TypeEvent();
                thisEvent.AddListener(listener);

                _eventTables.Add(event_id, thisEvent);
            }
        }

        public static void UnregisterListener(int event_id, UnityAction<object> listener)
        {
            TypeEvent thisEvent = null;
            if(_eventTables.TryGetValue(event_id, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void Post(int event_id, object param = null)
        {
            TypeEvent thisEvent = null;
            if(_eventTables.TryGetValue(event_id, out thisEvent))
            {
                thisEvent.Invoke(param);
            }
        }

        public static void Clera(int event_id)
        {
            _eventTables.Remove(event_id);
        }

        public static void Clear()
        {
            _eventTables.Clear();
        }
    }
}
