using System;

namespace Lab5Games
{
    public abstract class Singleton<T> : IDisposable where T : class, new()
    {
        private static T _instance = null;
        
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        public virtual void Dispose()
        {
            _instance = null;
        }
    }
}
