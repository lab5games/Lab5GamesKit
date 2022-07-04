using System;
using UnityEngine;

namespace Lab5Games
{
    public class LevelOperation : Schedule, IAwaitable<LevelOperation>, IAwaiter
    {
        AsyncOperation _asyncOp;

        bool _levelLoaded;
        bool _visibleOnLoaded;

        public event Action<LevelOperation> onLoaded;

        public string LevelName { get; private set; }

        public float Progress
        {
            get
            {
                if (_asyncOp == null)
                    return 0;

                return Mathf.Clamp01(_asyncOp.progress / 0.9f);
            }
        }

        public bool IsCompleted
        {
            get
            {
                return Status == ScheduleStatus.Completed || Status == ScheduleStatus.Canceled;
            }
        }

        public LevelOperation(string levelName, AsyncOperation asyncOp, bool visibleOnLoaded)
        {
            if (asyncOp == null)
                throw new ArgumentNullException(nameof(asyncOp));
            
            LevelName = levelName;

            _asyncOp = asyncOp;
            _asyncOp.allowSceneActivation = visibleOnLoaded;

            _levelLoaded = false;
            _visibleOnLoaded = visibleOnLoaded;
        }

        public override void Start()
        {
            if (Status == ScheduleStatus.Ready)
            {
                if (ScheduleSystem.Instance == null)
                {
                    GLogger.LogAsType("There needs a ScheduleSystem to start this schedule", GLogType.Error);
                    return;
                }

                Status = ScheduleStatus.Running;

                ScheduleSystem.Instance.AddLevelOperation(this);
            }
        }

        public override void Complete()
        {
            GLogger.LogAsType("This operation is no used!", GLogType.Warning);
        }

        public override void Cancel()
        {
            GLogger.LogAsType("This operation is no used!", GLogType.Warning);
        }

        public override void Pause()
        {
            GLogger.LogAsType("This operation is no used!", GLogType.Warning);
        }

        public override void Unpause()
        {
            GLogger.LogAsType("This operation is no used!", GLogType.Warning);
        }

        public void SetVisible()
        {
            if(!_visibleOnLoaded && _asyncOp.progress >= 0.9f)
            {
                _asyncOp.allowSceneActivation = true;

                base.Complete();
            }
        }

        public override void Tick(float dt)
        {
            if(Progress == 1f)
            {
                if(!_levelLoaded)
                {
                    _levelLoaded = true;
                    onLoaded?.Invoke(this);
                }
            }

            if(_visibleOnLoaded && _asyncOp.isDone)
            {
                base.Complete();
            }
        }

        public void GetResult()
        {
        }

        public void OnCompleted(Action continuation)
        {
            onCompleted += continuation;
        }

        public LevelOperation GetAwaiter()
        {
            return this;
        }
    }
}
