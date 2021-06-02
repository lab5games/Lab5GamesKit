using UnityEngine;
using UnityEditor;

namespace Lab5Games.Editor
{
    [CustomEditor(typeof(LevelReference))]
    public class LevelReferenceInspector : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(25);

            if (GUILayout.Button("Build Levels"))
            {
                (target as LevelReference).BuildLevels();
            }
        }
    }
}
