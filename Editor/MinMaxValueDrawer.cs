using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

namespace Lab5Games.Editor
{
    public class MinMaxValueDrawer : OdinValueDrawer<MinMaxValue>
    {
		InspectorProperty min;
		InspectorProperty max;

        protected override void Initialize()
        {
			min = this.Property.Children["min"];
			max = this.Property.Children["max"];
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
			Rect rect = EditorGUILayout.GetControlRect();

			if (label != null)
			{
				rect = EditorGUI.PrefixLabel(rect, label);
			}

			rect = EditorGUILayout.GetControlRect();

			GUIHelper.PushLabelWidth(75);
			min.ValueEntry.WeakSmartValue = SirenixEditorFields.FloatField(
				rect.Split(0, 2),
				"Min",
				(float)min.ValueEntry.WeakSmartValue);
			max.ValueEntry.WeakSmartValue = SirenixEditorFields.FloatField(
				rect.Split(1, 2),
				"Max",
				(float)max.ValueEntry.WeakSmartValue);
			GUIHelper.PopLabelWidth();
		}
    }
}
