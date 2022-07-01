using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Lab5Games
{
    public class LevelManager : ComponentSingleton<LevelManager>
    {
        static List<LevelOperation> _loadingLevels = new List<LevelOperation>();

        public static LevelOperation LoadLevel(string levelName, LoadSceneMode mode, bool visibleOnLoaded = true)
        {
            GLogger.LogToFilter($"[LevelManager] Load {levelName} level...", GLogFilter.System);

            var asyncOp = SceneManager.LoadSceneAsync(levelName, mode);
            LevelOperation schedule = new LevelOperation(levelName, asyncOp, visibleOnLoaded);

            _loadingLevels.Add(schedule);

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
            for(int i=_loadingLevels.Count-1; i>=0; i--)
            {
                _loadingLevels[i].Tick();

                if (_loadingLevels[i].IsCompleted)
                    _loadingLevels.RemoveAt(i);
            }
        }
    }
}
