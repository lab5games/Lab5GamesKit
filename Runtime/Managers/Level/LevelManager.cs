using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lab5Games
{
    public static class LevelManager 
    {
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
            LevelOperation levelOp = new LevelOperation(levelName, asyncOp, visibleOnLoaded);

            levelOp.Start();

            return levelOp;
        }

        public static LevelOperation LoadLevel(LevelReference levelRef, LoadSceneMode mode, bool visibleOnLoaded = true)
        {
            GLogger.LogToFilter($"[LevelManager] Load {levelRef.LevelName} level...", GLogFilter.System);

            var asyncOp = SceneManager.LoadSceneAsync(levelRef.LevelName, mode);
            LevelOperation levelOp = new LevelOperation(levelRef.LevelName, asyncOp, visibleOnLoaded);

            levelOp.Start();

            return levelOp;
        }

        public static async void UnloadLevel(LevelReference levelRef)
        {
            GLogger.LogToFilter($"[LevelManager] Unload {levelRef.LevelName} level...", GLogFilter.System);

            await SceneManager.UnloadSceneAsync(levelRef.LevelName);
        }

        public static async void UnloadLevel(string levelName)
        {
            GLogger.LogToFilter($"[LevelManager] Unload {levelName} level...", GLogFilter.System);

            await SceneManager.UnloadSceneAsync(levelName);
        }
    }
}
