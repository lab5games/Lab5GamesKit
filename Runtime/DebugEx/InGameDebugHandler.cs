using System;
using System.Text;

namespace Lab5Games
{
    public class InGameDebugHandler : ILogHandler
    {
        StringBuilder _strBuilder;

        readonly string[] LOG_COLORS = new string[]
        {
            "#FF0066FF",    // Error
            "#FFFF66FF",    // Warning
            "#66FFFFFF",    // Trace
            "#FFFFCCFF"     // Log
        };

        const string FORMAT = "<color={0}>[{1}] {2}</color>";

        public InGameDebugHandler()
        {
            _strBuilder = new StringBuilder();
        }

        public void Log(ELogType type, string log)
        {
            _strBuilder.Clear();

            _strBuilder.AppendFormat(FORMAT,
                LOG_COLORS[(int)type],
                DateTime.Now.ToString("HH:mm:ss"),
                log);

            InGameDebugConsole.Log(_strBuilder.ToString());
        }
    }
}
