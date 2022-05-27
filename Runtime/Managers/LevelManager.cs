using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Lab5Games
{
    public class LevelManager : Singleton<LevelManager>
    {
        static List<LevelSchedule> _schedules = new List<LevelSchedule>();

        public static LevelSchedule LoadLevel(string levelName, LoadSceneMode mode, bool visibleOnLoaded = true)
        {
            GLogger.LogToFilter($"[LevelManager] Load {levelName} level...", GLogFilter.System);

            var asyncOp = SceneManager.LoadSceneAsync(levelName, mode);
            LevelSchedule schedule = new LevelSchedule(levelName, asyncOp, visibleOnLoaded);

            _schedules.Add(schedule);

            return schedule;
        }

        public static async void UnloadLevel(string levelName)
        {
            GLogger.LogToFilter($"[LevelManager] Unload {levelName} level...", GLogFilter.System);

            await SceneManager.UnloadSceneAsync(levelName);
        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene scene)
        {
            GLogger.LogToFilter($"[LevelManager] {scene.name} level unloaded", GLogFilter.System, this);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GLogger.LogToFilter($"[LevelManager] {scene.name} level loaded", GLogFilter.System, this);
        }

        private void Update()
        {
            for(int i=_schedules.Count-1; i>=0; i--)
            {
                _schedules[i].Tick();

                if (_schedules[i].IsCompleted)
                    _schedules.RemoveAt(i);
            }
        }
    }
}
