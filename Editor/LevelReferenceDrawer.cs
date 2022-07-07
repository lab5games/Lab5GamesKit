using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

namespace Lab5Games.Editor
{
    public class LevelReferenceDrawer : OdinValueDrawer<LevelReference>
    {
        InspectorProperty sceneAssetProperty;
        InspectorProperty levelNameProperty;

        protected override void Initialize()
        {
            sceneAssetProperty = this.Property.Children["_sceneAsset"];
            levelNameProperty = this.Property.Children["_levelName"];
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            EditorGUILayout.Space();

            SirenixEditorGUI.BeginBox();

            Rect rect = EditorGUILayout.GetControlRect();

            if(label != null)
                rect = EditorGUI.PrefixLabel(rect, label);

            EditorGUI.BeginChangeCheck();
            {
                sceneAssetProperty.ValueEntry.WeakSmartValue =
                    SirenixEditorFields.UnityObjectField(
                        rect,
                        (SceneAsset)sceneAssetProperty.ValueEntry.WeakSmartValue,
                        typeof(SceneAsset),
                        false);
            }

            var buildScene = GetBuildScene((SceneAsset)sceneAssetProperty.ValueEntry.WeakSmartValue);
            if(EditorGUI.EndChangeCheck())
            {
                if (buildScene.scene == null)
                    levelNameProperty.ValueEntry.WeakSmartValue = string.Empty;
            }

            DrawSceneInfo(rect, buildScene);

            SirenixEditorGUI.EndBox();

            EditorGUILayout.Space();
        }

        private void DrawSceneInfo(Rect rect, BuildScene buildScene)
        {
            if(buildScene.assetGUID.Empty())
            {
                SirenixEditorGUI.ErrorMessageBox("Scene Asset cannot be NULL");
            }
            else
            {
                // scene status
                var iconContent = new GUIContent();
                var labelContent = new GUIContent();
                
                // missing from build scenes
                if (buildScene.buildIndex == -1)
                {
                    iconContent.image = EditorIcons.TestFailed;
                    labelContent.text = "Not In Build";
                }
                // in build scenes and enabled
                else if(buildScene.scene.enabled)
                {
                    iconContent.image = EditorIcons.TestPassed;
                    labelContent.text = $"Build Index: {buildScene.buildIndex}";

                }
                // in build scenes and disabled
                else
                {
                    iconContent.image = EditorIcons.TestFailed;
                    labelContent.text = "DISABLED";
                }

                rect = EditorGUILayout.GetControlRect();
                var labelWidth = GUIHelper.ActualLabelWidth;
                EditorGUI.LabelField(rect.AlignLeft(25), iconContent);
                rect.x += 25;
                EditorGUI.LabelField(rect.AlignLeft(labelWidth - 25), labelContent);

                // buttons
                rect.x -= 25;
                if(GUI.Button(rect.AlignRight(rect.width - labelWidth), "Build Settings"))
                {
                    EditorWindow.GetWindow(typeof(BuildPlayerWindow));
                }

                // asset path
                if (buildScene.buildIndex != -1)
                {
                    if(SirenixEditorGUI.MenuButton(0, buildScene.assetPath, true, EditorIcons.UnityFolderIcon))
                    {
                    }
                }
            }
        }

        struct BuildScene
        {
            public int buildIndex;
            public GUID assetGUID;
            public string assetPath;
            public EditorBuildSettingsScene scene;
        }

        static BuildScene GetBuildScene(SceneAsset sceneAsset)
        {
            var entry = new BuildScene
            {
                buildIndex = -1,
                assetGUID = new GUID(string.Empty),
                assetPath = string.Empty,
                scene = null
            };

            if (sceneAsset == null) return entry;

            entry.assetPath = AssetDatabase.GetAssetPath(sceneAsset);
            entry.assetGUID = AssetDatabase.GUIDFromAssetPath(entry.assetPath);

            var scenes = EditorBuildSettings.scenes;
            for(int indx=0; indx<scenes.Length; indx++)
            {
                if (!entry.assetGUID.Equals(scenes[indx].guid))
                    continue;

                entry.buildIndex = indx;
                entry.scene = scenes[indx];
                return entry;
            }

            return entry;
        }
    }
}
