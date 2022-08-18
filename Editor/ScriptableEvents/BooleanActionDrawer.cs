#if ODIN_INSPECTOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace Lab5Games.Editor
{
    public class BooleanActionDrawer : OdinValueDrawer<BooleanAction>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Rect rect = EditorGUILayout.GetControlRect();

            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            BooleanAction value = this.ValueEntry.SmartValue;
            GUIHelper.PushLabelWidth(20);
            value.value = EditorGUI.Toggle(rect, "", value.value);
            GUIHelper.PopLabelWidth();

            this.ValueEntry.SmartValue = value;
        }
    }
}
#endif