using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace Lab5Games.Editor
{
    public class ProjectSettingsEditor : EditorWindow
    {
        [MenuItem("Tools/Lab5Games/Project Settings", priority =0)]
        private static void OpenWindow()
        {
            EditorWindow.GetWindow<ProjectSettingsEditor>("ProjectSettings", true);
        }

        string _projectName;

        private void OnGUI()
        {
            _projectName = EditorGUILayout.TextField("Project", _projectName);

            EditorGUILayout.Space();
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
                throw new System.Exception($"The project [{_projectName}] has been existed.");
            }
            else
            {
                // root folder
                AssetDatabase.CreateFolder("Assets", _projectName);

                // sub folders
                string[] subFolders = new string[]
                {
                    "Animations",
                    "Animators",
                    "Scripts",
                    "Scenes",
                    "Prefabs",
                    "Materials",
                    "Textures",
                    "Sprites"
                };

                for(int i=0; i<subFolders.Length; i++)
                {
                    AssetDatabase.CreateFolder(root, subFolders[i]);
                }

                // add define symbols
                string[] symbols = new string[]
                {
                    "DEBUG_MODE"
                };

                string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                List<string> allDefines = definesString.Split(';').ToList();
                allDefines.AddRange(symbols.Except(allDefines));
                PlayerSettings.SetScriptingDefineSymbolsForGroup(
                    EditorUserBuildSettings.selectedBuildTargetGroup,
                    string.Join(";", allDefines.ToArray()));
            }
        }
    }
}
