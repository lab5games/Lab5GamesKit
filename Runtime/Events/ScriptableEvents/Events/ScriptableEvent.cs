using UnityEngine;
using System.Collections.Generic;

namespace Lab5Games.Events
{
    public abstract class ScriptableEvent<TObject> : ScriptableObject
    {
        internal const string MENU_NAME = "Lab5Games/Events/";

        readonly List<IScriptableEventListener<TObject>> _listeners = new List<IScriptableEventListener<TObject>>();

        public void Raise(TObject obj)
        {
            for(int i=_listeners.Count-1; i>=0; i--)
            {
                _listeners[i].OnEvent(obj);
            }
        }

        public void Register(IScriptableEventListener<TObject> listener)
        {
            if(!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void Unregister(IScriptableEventListener<TObject> listener)
        {
            _listeners.Remove(listener);
        }
    }
}
