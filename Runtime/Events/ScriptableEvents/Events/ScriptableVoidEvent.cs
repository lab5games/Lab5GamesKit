﻿using UnityEngine;
using UnityEngine.Events;

namespace Lab5Games.Events
{
    [System.Serializable]
    public struct Void { }

    [System.Serializable]
    public class UnityVoidEvent : UnityEvent<Void> { }

    [CreateAssetMenu(fileName ="New Void Event", menuName =MENU_NAME + "Void Event")]
    public class ScriptableVoidEvent : ScriptableEvent<Void>
    {
        readonly Void data = new Void();

        public void Raise() => Raise(data);
    }
}
