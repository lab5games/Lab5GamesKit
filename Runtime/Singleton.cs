using UnityEngine;

namespace Lab5Games
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if(_applicationIsQuitting)
                {
                    Debug.LogWarning($"[Singleton] {typeof(T).Name} instance already destroyed on application quit.");

                    return null;
                }

                if (_instance != null)
                {
                    return _instance;
                }

                _instance = FindObjectOfType<T>();

                if(_instance != null)
                {
                    return _instance;
                }

                CreateInstance();

                return _instance;
            }
        }

        private static void CreateInstance()
        {
            GameObject go = new GameObject();
            go.name = "[Singleton] " + typeof(T).Name;
            go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            _instance = go.AddComponent<T>();
        }

        private static void DestroyInstance()
        {
            _instance = null;
        }

        protected virtual void OnDestroy()
        {
            DestroyInstance();
        }

        private static bool _applicationIsQuitting = false;

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }
    }
}
