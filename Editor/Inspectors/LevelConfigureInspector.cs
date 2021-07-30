using UnityEngine;
using UnityEditor;

namespace Lab5Games.Editor
{
    [CustomEditor(typeof(LevelConfigure))]
    public class LevelConfigureInspector : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(25);

            if (GUILayout.Button("Build Levels"))
            {
                (target as LevelConfigure).BuildLevels();
            }
        }
    }
}
