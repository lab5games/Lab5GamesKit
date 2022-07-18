using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;

namespace Lab5Games.Editor
{
    public class VersionCodeDrawer : OdinValueDrawer<VersionCode>
    {
        string displayVersion;

        InspectorProperty property_Major;
        InspectorProperty property_Minor;
        InspectorProperty property_Revision;
        InspectorProperty property_Date;

        protected override void Initialize()
        {
            property_Major = this.ValueEntry.Property.Children["Major"];
            property_Minor = this.ValueEntry.Property.Children["Minor"];
            property_Revision = this.ValueEntry.Property.Children["Revision"];
            property_Date = this.ValueEntry.Property.Children["CreatedDate"];
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            SirenixEditorGUI.BeginBox(label);

            displayVersion = this.ValueEntry.SmartValue.Code;

            EditorGUI.BeginChangeCheck();
            {
                displayVersion = SirenixEditorFields.TextField("Code", displayVersion);
            }

            string[] codes = displayVersion.Split('.');

            bool isValidInput = false;

            if (codes.Length == 3)
            {
                isValidInput = true;

                isValidInput &= int.TryParse(codes[0], out int major);
                isValidInput &= int.TryParse(codes[1], out int minor);
                isValidInput &= int.TryParse(codes[2], out int revision);

                if(isValidInput)
                {
                    property_Major.ValueEntry.WeakSmartValue = major;
                    property_Minor.ValueEntry.WeakSmartValue = minor;   
                    property_Revision.ValueEntry.WeakSmartValue = revision;
                    
                }
            }

            if(EditorGUI.EndChangeCheck())
            {
                if(isValidInput)
                {
                    property_Date.ValueEntry.WeakSmartValue = DateTime.Now.ToString("MM-dd-yyyy");
                    //Debug.Log("version created date changed");
                }
            }

            GUI.enabled = false;
            SirenixEditorFields.TextField("Full Version", this.ValueEntry.SmartValue.FullVersion);
            GUI.enabled = true;

            SirenixEditorGUI.EndBox();
        }
    }
}
