using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lab5Games
{
    public class LevelSchedule : Schedule, IAwaitable<LevelSchedule>, IAwaiter
    {
        public static LevelSchedule Create(string levelName, LevelOptions option, bool visibleOnLoaded = true, bool autoStart = true)
        {
            LevelSchedule schedule = new LevelSchedule(levelName, option, visibleOnLoaded);

            if (autoStart)
                schedule.Start();

            return schedule;
        }


        AsyncOperation _async;
        bool _visibleOnLoaded;

        public string Level { get; }
        public LevelOptions Option { get; }
        
        public float Progress
        {
            get
            {
                if (_async == null)
                    return 0;

                return Mathf.Clamp01(_async.progress / 0.9f);
            }
        }

        public LevelSchedule(string levelName, LevelOptions option, bool visibleOnLoaded)
        {
            Level = levelName;
            Option = option;
            _visibleOnLoaded = visibleOnLoaded;
        }

        public override void Start()
        {
            if(Status == ScheduleStatus.Ready)
            {
                Status = ScheduleStatus.Running;

                switch(Option)
                {
                    case LevelOptions.LoadSingle:
                    case LevelOptions.LoadAdditive:
                        LoadLevel();
                        break;
                    case LevelOptions.Unload:
                        UnloadLevel();
                        break;
                }

                ScheduleSystem.Instance.AddSchedule(this);
            }
        }

        private void LoadLevel()
        {
            GLogger.LogAsType($"[LevelSchedule] Loading {Level} level...", LogType.Log);

            _async = SceneManager.LoadSceneAsync(Level, Option == LevelOptions.LoadSingle ? LoadSceneMode.Single : LoadSceneMode.Additive);

            _async.allowSceneActivation = _visibleOnLoaded;

            
        }

        private void UnloadLevel()
        {
            GLogger.LogAsType($"[LevelSchedule] Unloading {Level} level...", LogType.Log);

            _async = SceneManager.UnloadSceneAsync(Level);
        }

        public void SetLevelVisible()
        {
            if (Option == LevelOptions.Unload)
                return;

            if (_async == null)
                return;

            if(!_async.allowSceneActivation && _async.progress >= 0.9f)
            {
                _async.allowSceneActivation = true;

                Complete();

                GLogger.LogAsType($"[LevelSchedule] {Level} level loaded and set as visible. Option= {Option}", LogType.Log);
            }
        }

        public override void Tick(float dt)
        {
            if(Option == LevelOptions.Unload)
            {
                if (_async.isDone)
                {
                    Complete();

                    GLogger.LogAsType($"[LevelSchedule] {Level} level unloaded.", LogType.Log);
                }
            }
            else
            {
                if(_visibleOnLoaded && _async.isDone)
                {
                    Complete();

                    GLogger.LogAsType($"[LevelSchedule] {Level} level loaded. Option= {Option}", LogType.Log);
                }
            }
        }

        public bool IsCompleted
        {
            get
            {
                return Status == ScheduleStatus.Completed || Status == ScheduleStatus.Canceled;
            }
        }

        public void GetResult()
        {
            
        }

        public void OnCompleted(Action continuation)
        {
            onCompleted += continuation;
            onCanceled += continuation;
        }

        public LevelSchedule GetAwaiter()
        {
            return this;
        }
    }
}
