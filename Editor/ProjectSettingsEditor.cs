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

        string _projectName = "";
        bool _debugMode = false;


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

        const string DEBUG_MODE = "DEBUG_MODE";

        private void OnEnable()
        {
            _debugMode = IsDebugMode();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            _projectName = EditorGUILayout.TextField("Project Name", _projectName);
            _debugMode = EditorGUILayout.Toggle("Debug Mode", _debugMode);

            ToggleDebugMode(_debugMode);

            EditorGUILayout.Space(20);
            if(GUILayout.Button("New Project"))
            {
                NewProject();
            }
        }


        private void NewProject()
        {
            if (string.IsNullOrEmpty(_projectName))
                throw new System.Exception("Invalid project name.");

            string root = "Assets/" + _projectName;

            if(AssetDatabase.IsValidFolder(root))
            {
                throw new System.Exception($"The project folder [{_projectName}] has been existed.");
            }
            else
            {
                // root folder
                AssetDatabase.CreateFolder("Assets", _projectName);

                // sub folders
                for(int i=0; i<SUB_FOLDERS.Length; i++)
                {
                    AssetDatabase.CreateFolder(root, SUB_FOLDERS[i]);
                }

                BuildVersionProcess.ResetBuildVersion();
            }
        }

        private void ToggleDebugMode(bool toggle)
        {
            

            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            
            List<string> allDefines = definesString.Split(';').ToList();
            
            if(toggle)
            {
                if (!allDefines.Contains(DEBUG_MODE)) allDefines.Add(DEBUG_MODE);
            }
            else
            {
                if (allDefines.Contains(DEBUG_MODE)) allDefines.Remove(DEBUG_MODE);
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(
                    EditorUserBuildSettings.selectedBuildTargetGroup,
                    string.Join(";", allDefines.ToArray()));
        }

        bool IsDebugMode()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            List<string> allDefines = definesString.Split(';').ToList();

            return allDefines.Contains(DEBUG_MODE);
        }
    }
}
