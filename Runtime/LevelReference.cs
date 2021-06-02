using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
#endif

namespace Lab5Games
{
    [CreateAssetMenu(fileName ="New Level Reference", menuName ="Lab5Games/Level Reference")]
    public class LevelReference : ScriptableObject
    {
        [System.Serializable]
        public class Level
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

            _levels = new Level[_sceneAssets.Length];

            for (int i = 0; i < _sceneAssets.Length; i++)
            {
                string path = AssetDatabase.GetAssetPath(_sceneAssets[i]);
                string guid = AssetDatabase.AssetPathToGUID(path);
                string sceneName = Path.GetFileNameWithoutExtension(path);
                int sceneBuildIndex = SceneUtility.GetBuildIndexByScenePath(path);

                Level newLevel = new Level();
                newLevel.GUID = guid;
                newLevel.Path = path;
                newLevel.SceneName = sceneName;
                newLevel.SceneBuildIndex = sceneBuildIndex;

                _levels[i] = newLevel;

                DebugEx.Log(ELogType.Trace, $"Build level...({path}).");
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif

        [SerializeField] Level[] _levels;

        public Level[] levels
        {
            get { return _levels; }
        }
    }
}
