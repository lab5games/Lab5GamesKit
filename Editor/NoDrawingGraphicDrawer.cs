using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Lab5GamesEditor
{
    [CanEditMultipleObjects, CustomEditor(typeof(Lab5Games.NoDrawingGraphic), false)]
    public class NoDrawingGraphicDrawer : GraphicEditor
    {
		public override void OnInspectorGUI()
		{
			base.serializedObject.Update();
			EditorGUILayout.PropertyField(base.m_Script, new GUILayoutOption[0]);
			// skipping AppearanceControlsGUI
			base.RaycastControlsGUI();
			base.serializedObject.ApplyModifiedProperties();
		}
	}
}
