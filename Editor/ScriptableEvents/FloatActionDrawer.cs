using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace Lab5Games.Editor
{
    public class FloatActionDrawer : OdinValueDrawer<FloatAction>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Rect rect = EditorGUILayout.GetControlRect();

            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            FloatAction value = this.ValueEntry.SmartValue;
            GUIHelper.PushLabelWidth(20);
            value.value = EditorGUI.FloatField(rect, "", value.value);
            GUIHelper.PopLabelWidth();

            this.ValueEntry.SmartValue = value;
        }
    }
}
