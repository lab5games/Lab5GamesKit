using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;

namespace Lab5Games.Editor
{
    public class BuildVersionProcess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild(BuildReport report)
        {
            string currentVersion = PlayerSettings.bundleVersion;

            try
            {
                int major = Convert.ToInt32(currentVersion.Split('.')[0]);
                int minor = Convert.ToInt32(currentVersion.Split('.')[1]);
                int build = Convert.ToInt32(currentVersion.Split('.')[2]) + 1;

                PlayerSettings.bundleVersion = $"{major}.{minor}.{build}";

                if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
                {
                    PlayerSettings.iOS.buildNumber = build.ToString();
                }
                else if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                {
                    PlayerSettings.Android.bundleVersionCode = build;
                }
            }
            catch(Exception e)
            {
                UnityEngine.Debug.LogException(e);
                UnityEngine.Debug.LogError("BuildVersionProcess script failed. Make sure your current bundle version is in the format x.x.x (eg. 0.0.1).");
            }
        }

        public static void ResetBuildVersion()
        {
            PlayerSettings.bundleVersion = "0.0.0";

            Debug.LogWarning($"reset build version : 0.0.0");
        }
    }
}
