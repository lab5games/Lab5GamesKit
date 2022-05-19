using UnityEngine;
using UnityEngine.Events;

namespace Lab5Games.Events
{
    [System.Serializable]
    public class UnityFloatEvent : UnityEvent<float> { }

    [CreateAssetMenu(fileName ="New Float Event", menuName =MENU_NAME + "Float Event")]
    public class ScriptableFloatEvent : ScriptableEvent<float> { }
}
