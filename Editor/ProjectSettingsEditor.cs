using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace Lab5Games.Editor
{
    public class ProjectSettingsEditor : EditorWindow
    {
        [MenuItem("Lab5Games/Project Settings", priority = 0)]
        private static void OpenWindow()
        {
            EditorWindow.GetWindow<ProjectSettingsEditor>("ProjectSettings", true);
        }

        string _projectName = "New Project";
        bool _debugMode = false;
        bool _autoBuildVersion = false;


        readonly string[] SUB_FOLDERS = new string[]
        {
            "Animations",
            "Animators",
            "Scripts",
            "Scenes",
            "Scriptables",
            "Settings",
            "Shaders",
            "Prefabs",
            "Materials",
            "Textures",
            "Sprites"
        };

        const string DEBUG_MODE_SCRIPT_DEFINE_SYMBOL = "DEBUG_MODE";
        const string DEBUG_MODE_PLAYERPREFS_KEY = "debug_mode";
        const string AUTO_BUILD_VERSION_PLAYERPRES_KEY = "auto_build_version";

        void OnEnable()
        {
            _debugMode = IsDebugMode();
            _autoBuildVersion = IsAutoBuildVersion();
        }

        void OnGUI()
        {
            EditorGUILayout.Space();

            _projectName = EditorGUILayout.TextField("Project Name", _projectName);
            ToggleDebugMode(EditorGUILayout.Toggle("Debug Mode", _debugMode));
            ToggleAutoBuildVersion(EditorGUILayout.Toggle("Auto Build Version", _autoBuildVersion));

            EditorGUILayout.Space(20);
            if(GUILayout.Button("New Porject"))
            {
                NewProject();
            }
        }

        private void NewProject()
        {
            if (string.IsNullOrEmpty(_projectName))
                throw new System.Exception("Invalid project name.");

            string root = "Assets/" + _projectName;

            if (AssetDatabase.IsValidFolder(root))
            {
                throw new System.Exception($"The project folder [{_projectName}] has been existed.");
            }
            else
            {
                // root folder
                AssetDatabase.CreateFolder("Assets", _projectName);

                // sub folders
                for (int i = 0; i < SUB_FOLDERS.Length; i++)
                {
                    AssetDatabase.CreateFolder(root, SUB_FOLDERS[i]);
                }

                BuildVersionProcess.ResetBuildVersion();
            }
        }

        private void ToggleDebugMode(bool toggle)
        {
            if (_debugMode != toggle)
            {
                _debugMode = toggle;
                PlayerPrefs.SetInt(DEBUG_MODE_PLAYERPREFS_KEY, toggle ? 1 : 0);

                string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

                List<string> allDefines = definesString.Split(';').ToList();

                if (toggle)
                {
                    if (!allDefines.Contains(DEBUG_MODE_SCRIPT_DEFINE_SYMBOL)) allDefines.Add(DEBUG_MODE_SCRIPT_DEFINE_SYMBOL);
                }
                else
                {
                    if (allDefines.Contains(DEBUG_MODE_SCRIPT_DEFINE_SYMBOL)) allDefines.Remove(DEBUG_MODE_SCRIPT_DEFINE_SYMBOL);
                }

                PlayerSettings.SetScriptingDefineSymbolsForGroup(
                        EditorUserBuildSettings.selectedBuildTargetGroup,
                        string.Join(";", allDefines.ToArray()));
            }
        }

        private void ToggleAutoBuildVersion(bool toggle)
        {
            if(_autoBuildVersion != toggle)
            {
                _autoBuildVersion = toggle;
                PlayerPrefs.SetInt(AUTO_BUILD_VERSION_PLAYERPRES_KEY, toggle ? 1 : 0);
            }
        }

        public static bool IsDebugMode()
        {
            return PlayerPrefs.GetInt(DEBUG_MODE_PLAYERPREFS_KEY, 0) == 1;
        }

        public static bool IsAutoBuildVersion()
        {
            return PlayerPrefs.GetInt(AUTO_BUILD_VERSION_PLAYERPRES_KEY, 0) == 1;
        }
    }
}
