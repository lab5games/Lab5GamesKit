using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace Lab5Games.Editor
{
    public class Vector2ActionDrawer : OdinValueDrawer<Vector2Action>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Rect rect = EditorGUILayout.GetControlRect();

            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            Vector2Action value = this.ValueEntry.SmartValue;
            GUIHelper.PushLabelWidth(20);
            value.value = EditorGUI.Vector2Field(rect, "", value.value);
            GUIHelper.PopLabelWidth();

            this.ValueEntry.SmartValue = value;
        }
    }
}
