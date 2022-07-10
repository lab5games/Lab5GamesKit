using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Lab5Games
{
    [System.Serializable]
    public class LevelReference : ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        [SerializeField]
        private SceneAsset _sceneAsset;

        public bool IsVaildSceneAsset
        {
            get => _sceneAsset != null;
        }

        private string GetLevelNameFromAsset()
        {
            return _sceneAsset == null ? string.Empty : AssetDatabase.GetAssetPath(_sceneAsset);
        }
        
        private SceneAsset GetSceneAssetFromLevelName()
        {
            return string.IsNullOrEmpty(_levelName) ? null : AssetDatabase.LoadAssetAtPath<SceneAsset>(_levelName);
        }

        private void HandleBeforeSerialize()
        {
            if (!IsVaildSceneAsset && !string.IsNullOrEmpty(_levelName))
            {
                _sceneAsset = GetSceneAssetFromLevelName();
                _levelName = GetLevelNameFromAsset();

                EditorSceneManager.MarkAllScenesDirty();
            }
            else
            {
                _levelName = GetLevelNameFromAsset();
            }
        }

        private void HandleAfterSerialize()
        {
            EditorApplication.update -= HandleAfterSerialize;

            if (IsVaildSceneAsset)
            {
                return;
            }

            if (string.IsNullOrEmpty(_levelName)) return;

            _sceneAsset = GetSceneAssetFromLevelName();

            if(!IsVaildSceneAsset) _levelName = string.Empty;

            if (!Application.isPlaying) EditorSceneManager.MarkAllScenesDirty();
        }
#endif

        [SerializeField]
        private string _levelName;

        public string LevelName
        {
            get => _levelName;
        }

        public static implicit operator string(LevelReference levelReference)
        {
            return levelReference.LevelName;
        }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            HandleBeforeSerialize();
#endif
        }

        public void OnAfterDeserialize()
        {
#if UNITY_EDITOR
            EditorApplication.update += HandleAfterSerialize;
#endif
        }
    }
}
