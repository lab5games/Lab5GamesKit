﻿using UnityEngine;

namespace Lab5Games
{
    internal static class Logger
    {
        public static void LogAsType(string log, LogType type, UnityEngine.Object context = null)
        {
            Debug.Log(log + "\nCPAPI:{\"cmd\":\"LogType\", \"name\":\"" + type.ToString() + "\"}", context);
        }

        public static void LogToFilter(string log, LogFilter filter, UnityEngine.Object context = null)
        {
            Debug.Log(log + "\nCPAPI:{\"cmd\":\"Filter\", \"name\":\"" + filter.ToString() + "\"}", context);
        }
    }
}