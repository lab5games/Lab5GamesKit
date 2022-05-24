using UnityEngine;

namespace Lab5Games
{
    public static class GLogger
    {
        public static void LogAsType(string log, GLogType type, UnityEngine.Object context = null)
        {
            Debug.Log(log + "\nCPAPI:{\"cmd\":\"LogType\", \"name\":\"" + type.ToString() + "\"}", context);
        }

        public static void LogToFilter(string log, GLogFilter filter, UnityEngine.Object context = null)
        {
            Debug.Log(log + "\nCPAPI:{\"cmd\":\"Filter\", \"name\":\"" + filter.ToString() + "\"}", context);
        }
    }
}
