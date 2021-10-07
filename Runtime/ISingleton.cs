using UnityEngine;

namespace Lab5Games
{
    public abstract class ISingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _singleton = null;

        public static T Instance
        {
            get
            {
                if(_applicationIsQuitting)
                {
                    Debug.LogWarning($"[Singleton] {typeof(T).Name} instance already destroyed on application quit.");

                    return null;
                }

                if (_singleton != null)
                {
                    return _singleton;
                }

                _singleton = FindObjectOfType<T>();

                if(_singleton != null)
                {
                    return _singleton;
                }

                CreateInstance();

                return _singleton;
            }
        }

        private static void CreateInstance()
        {
            GameObject go = new GameObject();
            go.name = "[Singleton] " + typeof(T).Name;
            go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            _singleton = go.AddComponent<T>();
        }
        
        public virtual bool IsPersistent { get { return true; } }

        private void Awake()
        {
            if (_singleton != null && _singleton != this)
            {
                Destroy(gameObject);
            }

            if (IsPersistent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private static bool _applicationIsQuitting = false;

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }
    }
}
