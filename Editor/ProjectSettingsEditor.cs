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

        void OnGUI()
        {
            EditorGUILayout.Space();

            _projectName = EditorGUILayout.TextField("Project Name", _projectName);

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

                // add debug define symbol
                string strDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

                List<string> allDefines = strDefines.Split(';').ToList();

                if (!allDefines.Contains(DEBUG_MODE_SCRIPT_DEFINE_SYMBOL))
                    allDefines.Add(DEBUG_MODE_SCRIPT_DEFINE_SYMBOL);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", allDefines.ToArray()));


                AssetDatabase.Refresh();

                Debug.Log($"{_projectName} project created");
            }
        }
    }
}
