using UnityEngine;
using UnityEngine.Events;

namespace Lab5Games.Events
{
    public interface IScriptableEventListener<TObject>
    {
        void OnEvent(TObject obj);
    }

    public abstract class ScriptableEventListener<TObject, E, H> : MonoBehaviour, IScriptableEventListener<TObject> where E : ScriptableEvent<TObject> where H : UnityEvent<TObject>
    {
        [SerializeField] private E GameEvent;
        [SerializeField] private H Handler;


        public virtual void OnEvent(TObject obj)
        {
            if(Handler != null)
            {
                Handler?.Invoke(obj);
            }
        }

        void OnEnable()
        {
            if(GameEvent != null)
            {
                GameEvent.Register(this);
            }
        }

        void OnDisable()
        {
            if(GameEvent != null)
            {
                GameEvent.Unregister(this);
            }
        }
    }
}
