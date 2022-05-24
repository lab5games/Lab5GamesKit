using UnityEngine;

namespace Lab5Games
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public virtual bool IsPersistent => false;

        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if(Instance != null)
            {
                Logger.LogAsType($"[Singleton] There should never be more than one {typeof(T).Name} in the scene", LogType.Warning);
                Destroy(gameObject);
                return;
            }


            Instance = this as T;

            if (IsPersistent)
                DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            Logger.LogAsType($"[Singleton] {typeof(T).Name} instance  destroyed", LogType.Log, this);
        }
    }
}
