using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Unity.EditorCoroutines.Editor;

namespace Lab5Games.Editor
{
    public class BuildTools : OdinEditorWindow
    {
        [MenuItem("Tools/Lab5Games/Build Tools", priority =1)]
        static void OpenWindow()
        {
            GetWindow<BuildTools>().Show();
        }

        [System.Serializable]
        public class TargetPlatform
        {
            [ToggleGroup("Enabled", "$Label")]
            public bool Enabled;

            public string Label => BuildTarget.ToString();

            public readonly BuildTarget BuildTarget;
            public readonly BuildTargetGroup TargetGroup;

            public TargetPlatform(BuildTarget buildTarget)
            {
                BuildTarget = buildTarget;
                TargetGroup = GetTargetGroupForTarget(buildTarget);
            }
        }

        [Title("Platforms to Build"), PropertySpace(SpaceAfter =10)]
        [LabelText("Platform"), ListDrawerSettings(HideAddButton =true, HideRemoveButton =true, Expanded =true, DraggableItems =false, ShowItemCount =false)]
        public List<TargetPlatform> TargetsToBuild = new List<TargetPlatform>();

        readonly BuildTarget[] BuildTargets = new BuildTarget[]
        {
            BuildTarget.StandaloneWindows64,
            BuildTarget.StandaloneOSX,
            BuildTarget.StandaloneLinux64,
            BuildTarget.Android,
            BuildTarget.iOS,
            BuildTarget.WebGL
        };


        static BuildTargetGroup GetTargetGroupForTarget(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.StandaloneLinux64:
                case BuildTarget.StandaloneOSX:
                    return BuildTargetGroup.Standalone;

                case BuildTarget.iOS:
                    return BuildTargetGroup.iOS;

                case BuildTarget.Android:
                    return BuildTargetGroup.Android;

                case BuildTarget.WebGL:
                    return BuildTargetGroup.WebGL;

                default:
                    return BuildTargetGroup.Unknown;
            }
        }

        protected override void Initialize()
        {
            TargetsToBuild.Clear();

            foreach(var target in BuildTargets)
            {
                // skip if unsupported
                if (!BuildPipeline.IsBuildTargetSupported(GetTargetGroupForTarget(target), target))
                    continue;

                TargetsToBuild.Add(new TargetPlatform(target)); 
            }
        }

        bool BuildValidation()
        {
            foreach (var platform in TargetsToBuild)
            {
                if (platform.Enabled)
                    return false;
            }

            return true;
        }

        [HideIf("$BuildValidation")]
        [Button("Build Selected Platforms", ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1f)]
        void Build()
        {
            List<TargetPlatform> selectedPlatforms = new List<TargetPlatform>();
            foreach(var platform in TargetsToBuild)
            {
                if (platform.Enabled)
                    selectedPlatforms.Add(platform);
            }

            if(selectedPlatforms.Count > 0)
            {
                EditorCoroutineUtility.StartCoroutine(Build(selectedPlatforms), this);
            }
        }

        IEnumerator Build(List<TargetPlatform> targetsToBuild)
        {
            // show the progress display
            int buildAllProcessID = Progress.Start("Build All", "Building all selected platforms", Progress.Options.Sticky);
            Progress.ShowDetails();
            yield return new EditorWaitForSeconds(.5f);

            BuildTarget originalTarget = EditorUserBuildSettings.activeBuildTarget;

            // build each targets
            for (int indx = 0; indx < targetsToBuild.Count; indx++)
            {
                var buildTarget = targetsToBuild[indx].BuildTarget;

                Progress.Report(buildAllProcessID, indx + 1, targetsToBuild.Count);
                int buildTaskProgressID = Progress.Start($"Build {buildTarget.ToString()}", null, Progress.Options.Sticky, buildAllProcessID);
                yield return new EditorWaitForSeconds(.5f);

                // perform the build
                if (!BuildIndividualTarget(buildTarget))
                {
                    Progress.Finish(buildTaskProgressID, Progress.Status.Failed);
                    Progress.Finish(buildAllProcessID, Progress.Status.Failed);

                    if (EditorUserBuildSettings.activeBuildTarget != originalTarget)
                        EditorUserBuildSettings.SwitchActiveBuildTarget(GetTargetGroupForTarget(originalTarget), originalTarget);

                    yield break;
                }

                Progress.Finish(buildTaskProgressID, Progress.Status.Succeeded);
                yield return new EditorWaitForSeconds(.5f);
            }

            Progress.Finish(buildAllProcessID, Progress.Status.Succeeded);

            yield return null;
        }

        bool BuildIndividualTarget(BuildTarget target)
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            // get the list of scenes
            List<string> scenes = new List<string>();
            foreach (var scene in EditorBuildSettings.scenes)
                scenes.Add(scene.path);

            // configure the options
            options.scenes = scenes.ToArray();
            options.target = target;
            options.targetGroup = GetTargetGroupForTarget(target);

            // set the location path
            if (target == BuildTarget.Android)
            {
                string apkName = PlayerSettings.productName + ".apk";
                options.locationPathName = Path.Combine("Builds", target.ToString(), apkName);
            }
            else
            {
                options.locationPathName = Path.Combine("Builds", target.ToString(), PlayerSettings.productName);
            }

            if (BuildPipeline.BuildCanBeAppended(target, options.locationPathName) == CanAppendBuild.Yes)
                options.options = BuildOptions.AcceptExternalModificationsToPlayer;
            else
                options.options = BuildOptions.None;

            // start the build
            BuildReport report = BuildPipeline.BuildPlayer(options);

            // was the build successful?
            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"Build for {target.ToString()} platform completed in {report.summary.totalTime.Seconds} seconds");
                return true;
            }

            Debug.LogError($"Build for {target.ToString()} platform failed");

            return false;
        }

    }
}
