using UnityEngine;
using UnityEngine.Events;

namespace Lab5Games.Events
{
    [System.Serializable]
    public struct Void
    {
        public static readonly Void Default = new Void();
    }

    [System.Serializable]
    public class UnityVoidEvent : UnityEvent<Void> { }

    [CreateAssetMenu(fileName ="New Void Event", menuName =MENU_NAME + "Void Event")]
    public class VoidEvent : ScriptableEvent<Void>
    {
        public void Raise() => Raise(Void.Default);
    }
}
