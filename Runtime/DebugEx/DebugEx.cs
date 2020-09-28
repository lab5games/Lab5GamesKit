using System;
using System.Diagnostics;

namespace Lab5Games
{
    public enum ELogType
    {
        Error,
        Warning,
        Trace,
        Log
    }

    public static class DebugEx
    {
        private static ILogger _logger;
        public static ILogger logger { get { return _logger; } }

        static DebugEx()
        {
            _logger = new Logger(new LogHandler());
        }

        [Conditional("DEBUG_MODE")]
        public static void Log(ELogType type, object log)
        {
            _logger.Log(type, log);
        }
    }
}
