using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

namespace Lab5Games.Editor
{
    public class MinMaxValueDrawer : OdinValueDrawer<Lab5Games.MinMaxValue>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
			Rect rect = EditorGUILayout.GetControlRect();

			if (label != null)
			{
				rect = EditorGUI.PrefixLabel(rect, label);
			}

			var value = this.ValueEntry.SmartValue;
			GUIHelper.PushLabelWidth(20);
			value.min = EditorGUI.FloatField(rect.AlignLeft(rect.width * 0.46f), "min", value.min);
			value.max = EditorGUI.FloatField(rect.AlignRight(rect.width * 0.46f), "max", value.max);
			GUIHelper.PopLabelWidth();

			this.ValueEntry.SmartValue = value;
		}
    }
}
