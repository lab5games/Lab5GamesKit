using UnityEngine;
using UnityEngine.Events;

namespace Lab5Games.Events
{
    [System.Serializable]
    public class UnityIntegerEvent : UnityEvent<int> { }

    [CreateAssetMenu(fileName ="New Integer Event", menuName =MENU_NAME + "Integer Event")]
    public class ScriptableIntegerEvent : ScriptableEvent<int> { }
}
