#if ODIN_INSPECTOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Lab5Games.Editor
{
    public class CreateFolders : OdinEditorWindow
    {
        [ShowInInspector]
        public static string projectName = "PROJECT_NAME";

        [MenuItem("Tools/Lab5Games/Create Default Folders", priority =0)]
        static void OpenWindow()
        {
            var window = GetWindow<CreateFolders>();

            window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150);
            window.ShowPopup();
        }

        static void CreateAllFolders()
        {
            List<string> folders = new List<string>()
            { 
                "Art/Animations",
                "Art/Materials",
                "Art/Models",
                "Art/Textures",
                "Art/UI",

                "Audio/Music",
                "Audio/Sound",

                "Code/Editor",
                "Code/Scripts",
                "Code/Shaders",

                "Docs",

                "Game/Fonts",
                "Game/Prefabs",
                "Game/Scenes",
                "Game/Settings",
                "Game/Scriptables",

                "../ThridParty"
            };

            if (string.IsNullOrEmpty(projectName))
                projectName = "PROJECT_NAME";

            foreach(string folder in folders)
            {
                if (!Directory.Exists("Assets/" + projectName + "/" + folder))
                {
                    Directory.CreateDirectory("Assets/" + projectName + "/" + folder);
                }
            }

            AssetDatabase.Refresh();    
        }

        [Button(ButtonSizes.Large)]
        void Create()
        {
            CreateAllFolders();
            Close();
        }
    }
}
#endif