﻿using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
#endif

namespace Lab5Games
{
    [CreateAssetMenu(fileName ="New Game Level Reference", menuName ="Lab5Games/Game Level Reference")]
    public class GameLevelReference : ScriptableObject
    {
        [System.Serializable]
        public class GameLevel
        {
            [SerializeField] string sceneName;
            [SerializeField] string sceneGUID;
            [SerializeField] int sceneBuildIndex;
            [SerializeField] string path;

            public string GUID
            {
                get { return this.sceneGUID; }
                internal set { this.sceneGUID = value; }
            }

            public string Path
            {
                get { return this.path; }
                internal set { this.path = value; }
            }

            public string SceneName
            {
                get { return this.sceneName; }
                internal set { this.sceneName = value; }
            }

            public int SceneBuildIndex
            {
                get { return this.sceneBuildIndex; }
                internal set { this.sceneBuildIndex = value; }
            }
        }


#if UNITY_EDITOR
        [SerializeField] SceneAsset[] _sceneAssets;

        public void BuildLevels()
        {
            if (_sceneAssets == null || _sceneAssets.Length <= 0)
            {
                return;
            }

            _levels = new GameLevel[_sceneAssets.Length];

            for (int i = 0; i < _sceneAssets.Length; i++)
            {
                string path = AssetDatabase.GetAssetPath(_sceneAssets[i]);
                string guid = AssetDatabase.AssetPathToGUID(path);
                string sceneName = Path.GetFileNameWithoutExtension(path);
                int sceneBuildIndex = SceneUtility.GetBuildIndexByScenePath(path);

                GameLevel newLevel = new GameLevel();
                newLevel.GUID = guid;
                newLevel.Path = path;
                newLevel.SceneName = sceneName;
                newLevel.SceneBuildIndex = sceneBuildIndex;

                _levels[i] = newLevel;

                DebugEx.Log(ELogType.Trace, $"build game level...({path}).");
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif

        [SerializeField] GameLevel[] _levels;

        public GameLevel[] levels
        {
            get { return _levels; }
        }
    }
}
