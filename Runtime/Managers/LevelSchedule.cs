using System;
using UnityEngine;

namespace Lab5Games
{
    public class LevelSchedule : IAwaitable<LevelSchedule>, IAwaiter
    {
        AsyncOperation _asyncOp;

        bool _visibleOnLoaded;

        public string Level { get; private set; }

        public float Progress
        {
            get
            {
                if (_asyncOp == null)
                    return 0;

                return Mathf.Clamp01(_asyncOp.progress / 0.9f);
            }
        }

        public bool IsCompleted { get; private set; }

        public event Action onCompleted;

        public LevelSchedule(string levelName, AsyncOperation asyncOp, bool visibleOnLoaded)
        {
            if (asyncOp == null)
                throw new ArgumentNullException(nameof(asyncOp));
            
            Level = levelName;

            _asyncOp = asyncOp;
            _asyncOp.allowSceneActivation = visibleOnLoaded;

            _visibleOnLoaded = visibleOnLoaded;
        }

        private void Complete()
        {
            IsCompleted = true;
            onCompleted?.Invoke();
        }

        public void SetVisible()
        {
            if(!_visibleOnLoaded && _asyncOp.progress >= 0.9f)
            {
                _asyncOp.allowSceneActivation = true;

                Complete();
            }
        }

        public void Tick()
        {
            if(_visibleOnLoaded && _asyncOp.isDone)
            {
                Complete();
            }
        }

        public void GetResult()
        {
        }

        public void OnCompleted(Action continuation)
        {
            onCompleted += continuation;
        }

        public LevelSchedule GetAwaiter()
        {
            return this;
        }
    }
}
