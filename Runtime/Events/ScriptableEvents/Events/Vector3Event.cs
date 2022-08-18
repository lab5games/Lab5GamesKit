using UnityEngine;
using UnityEngine.Events;

namespace Lab5Games.Events
{
    [System.Serializable]
    public class UnityVector3Event : UnityEvent<Vector3> { }

    [CreateAssetMenu(fileName ="New Vector3 Event", menuName =MENU_NAME + "Vector3 Event")]
    public class Vector3Event : ScriptableEvent<Vector3> { }
}
