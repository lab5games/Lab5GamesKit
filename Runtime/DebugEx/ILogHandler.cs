using System;
using System.Text;

namespace Lab5Games.Lab5GamesKit
{
    public interface ILogHandler
    {
        void Log(ELogType type, string log);
    }

    class LogHandler : ILogHandler
    {
        StringBuilder _strBuilder;
        UnityEngine.ILogger _uLogger;

        readonly string[] LOG_COLORS = new string[]
        {
            "#FF0066FF",    // Error
            "#FFFF66FF",    // Warning
            "#66FFFFFF",    // Trace
            "#FFFFCCFF"     // Log

        };

        const string FORMAT = "<b><size=14><color={0}>[{1}] {2}</color></size></b>";

        public LogHandler()
        {
            _strBuilder = new StringBuilder();
            _uLogger = UnityEngine.Debug.unityLogger;
        }

        public void Log(ELogType type, string log)
        {
            _strBuilder.Clear();

            _strBuilder.AppendFormat(FORMAT,
                LOG_COLORS[(int)type],
                DateTime.Now.ToString("HH:mm:ss"),
                log);

            _uLogger.Log(UnityEngine.LogType.Log, type.ToString(), _strBuilder.ToString());
        }
    }
}
