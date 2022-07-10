using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace Lab5Games.Editor
{
    public class VersionCodeDrawer : OdinValueDrawer<VersionCode>
    {
        string displayVersion;
        object key = new object();

        InspectorProperty property_Major;
        InspectorProperty property_Minor;
        InspectorProperty property_Revision;

        protected override void Initialize()
        {
            property_Major = this.ValueEntry.Property.Children["Major"];
            property_Minor = this.ValueEntry.Property.Children["Minor"];
            property_Revision = this.ValueEntry.Property.Children["Revision"];
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            displayVersion = this.ValueEntry.SmartValue.Version;
            displayVersion = SirenixEditorFields.TextField(label, displayVersion);

            SirenixEditorGUI.BeginShakeableGroup(key);

            string[] codes = displayVersion.Split('.');
            
            if(codes.Length == 3)
            {
                bool isValidInput = true;

                isValidInput &= int.TryParse(codes[0], out int major);
                isValidInput &= int.TryParse(codes[1], out int minor);
                isValidInput &= int.TryParse(codes[2], out int revision);

                if(isValidInput)
                {
                    property_Major.ValueEntry.WeakSmartValue = major;
                    property_Minor.ValueEntry.WeakSmartValue = minor;   
                    property_Revision.ValueEntry.WeakSmartValue = revision;
                }
                else
                {
                    SirenixEditorGUI.StartShakingGroup(key, 0.5f);
                }
            }
            else
            {
                SirenixEditorGUI.StartShakingGroup(key, 0.5f);
            }
            

            SirenixEditorGUI.EndShakeableGroup(key);
        }
    }
}
