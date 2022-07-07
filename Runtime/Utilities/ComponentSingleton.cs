using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Lab5Games
{
    public abstract class ComponentSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [ShowInInspector, PropertyOrder(-99)]
        public abstract bool IsPersistent { get; }

        private static T _istance = null;
        public static T Instance => _istance;

        protected virtual void Awake()
        {
            if(Instance != null)
            {
                GLogger.LogAsType($"[Singleton] There should never be more than one {typeof(T).Name} instance in the scene.", GLogType.Warning);
                Destroy(gameObject);
                
                return;
            }

            _istance = this as T;

            if (IsPersistent) DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            _istance = null;

            GLogger.LogToFilter($"[Singleton] {typeof(T).Name} instance is already destroyed", GLogFilter.System);
        }
    }
}
