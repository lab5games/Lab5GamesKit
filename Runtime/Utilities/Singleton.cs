using UnityEngine;

namespace Lab5Games
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public virtual bool IsPersistent => false;

        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this as T;

            Logger.LogAsType($"[Singleton] Instance {typeof(T).Name} created", LogType.Log, this);

            if (IsPersistent)
                DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            Logger.LogAsType($"[Singleton] Instance {typeof(T).Name} destroyed", LogType.Log, this);
        }
    }
}
