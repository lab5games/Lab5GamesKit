using UnityEngine;

namespace Lab5Games
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public virtual bool IsPersistent => false;

        public static T Instance { get; private set; } = null;

        protected virtual void Awake()
        {
            if(Instance != null)
            {
                GLogger.LogAsType($"[Singleton] There should never be more than one {typeof(T).Name} in the scene", GLogType.Warning);
                Destroy(gameObject);
                return;
            }


            Instance = this as T;

            if (IsPersistent)
                DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            Instance = null;

            GLogger.LogAsType($"[Singleton] {typeof(T).Name} instance  destroyed", GLogType.Log, this);
        }
    }
}
