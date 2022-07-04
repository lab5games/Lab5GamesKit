using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lab5Games
{
    public static class LevelManager 
    {
        static List<LevelOperation> _loadingLevels = new List<LevelOperation>();

        public static Action<string> levelLoaded;
        public static Action<string> levelUnloaded; 

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialzie()
        {
            SceneManager.sceneLoaded += OnLevelLoaded;
            SceneManager.sceneUnloaded += OnLevelUnloaded;
        }

        private static void OnLevelUnloaded(Scene levelScene)
        {
            GLogger.LogToFilter($"[LevelManager] Level({levelScene}) unloaded", GLogFilter.System);
        }

        private static void OnLevelLoaded(Scene levelScene, LoadSceneMode loadMode)
        {
            GLogger.LogToFilter($"[LevelManager] Level({levelScene}) loaded", GLogFilter.System);
        }

        public static LevelOperation LoadLevel(string levelName, LoadSceneMode mode, bool visibleOnLoaded = true)
        {
            GLogger.LogToFilter($"[LevelManager] Load {levelName} level...", GLogFilter.System);

            var asyncOp = SceneManager.LoadSceneAsync(levelName, mode);
            LevelOperation operation = new LevelOperation(levelName, asyncOp, visibleOnLoaded);

            _loadingLevels.Add(operation);

            return operation;
        }

        public static async void UnloadLevel(string levelName)
        {
            GLogger.LogToFilter($"[LevelManager] Unload {levelName} level...", GLogFilter.System);

            await SceneManager.UnloadSceneAsync(levelName);
        }
    }
}
