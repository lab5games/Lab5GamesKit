using UnityEngine;
using Unity.Collections;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
#endif

namespace Lab5Games
{
    [CreateAssetMenu(fileName ="New Level Configure", menuName ="Lab5Games/Level Configure")]
    public class LevelConfigure : ScriptableObject
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
        [SerializeField] SceneAsset[] _scenes;

        public void BuildLevels()
        {
            if (_scenes == null || _scenes.Length <= 0)
            {
                throw new System.Exception("No scenes in this LevelConfigure");
            }

            levels = new Level[_scenes.Length];

            for (int i = 0; i < _scenes.Length; i++)
            {
                string path = AssetDatabase.GetAssetPath(_scenes[i]);
                string guid = AssetDatabase.AssetPathToGUID(path);
                string sceneName = Path.GetFileNameWithoutExtension(path);
                int sceneBuildIndex = SceneUtility.GetBuildIndexByScenePath(path);

                if(sceneBuildIndex == -1)
                {
                    throw new System.Exception($"Please add scene({path}) to the BuildSettings(File -> Build Settings -> Scenes in Build).");
                }

                Level newLevel = new Level();
                newLevel.GUID = guid;
                newLevel.Path = path;
                newLevel.SceneName = sceneName;
                newLevel.SceneBuildIndex = sceneBuildIndex;

                levels[i] = newLevel;

                DebugEx.Log(ELogType.Trace, $"build game level...({path}).");
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif

        [Attributes.ReadOnly]
        public Level[] levels;
    }
}
