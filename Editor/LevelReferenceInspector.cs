using UnityEngine;
using UnityEditor;

namespace Lab5Games.Editor
{
    [CustomEditor(typeof(GameLevelReference))]
    public class LevelReferenceInspector : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(25);

            if (GUILayout.Button("Build Levels"))
            {
                (target as GameLevelReference).BuildLevels();
            }
        }
    }
}
