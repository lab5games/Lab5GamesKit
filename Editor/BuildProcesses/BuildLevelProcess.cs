using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;

namespace Lab5Games.Editor
{
    public class BuildLevelProcess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild(BuildReport report)
        {
            LevelConfigure levelConfig = Resources.Load<LevelConfigure>("LevelConfigure");

            try
            {
                if (levelConfig != null)
                {
                    levelConfig.BuildLevels();
                }
            }
            catch(Exception e)
            {
                Debug.LogException(e);
                Debug.LogError("BuildLevelProcess script failed.");
            }
        }
    }
}
