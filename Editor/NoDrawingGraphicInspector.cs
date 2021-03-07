using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Lab5Games.Editor
{
    [CanEditMultipleObjects, CustomEditor(typeof(NoDrawingGraphic), false)]
    public class NoDrawingGraphicInspector : GraphicEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Script, new GUILayoutOption[0]);
            RaycastControlsGUI();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
