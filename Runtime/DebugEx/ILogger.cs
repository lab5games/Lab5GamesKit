
namespace Lab5Games
{
    public interface ILogger
    {
        bool LogEnabled { get; set; }
        ELogType LogFilter { get; set; }
        ILogHandler LogHandler { get; set; }

        void Log(ELogType type, object log);
    }

    class Logger : ILogger
    {
        public bool LogEnabled { get; set; }
        public ELogType LogFilter { get; set; }
        public ILogHandler LogHandler { get; set; }

        public Logger(ILogHandler logHandler)
        {
            LogEnabled = true;
            LogFilter = ELogType.Log;
            LogHandler = logHandler;
        }

        public void Log(ELogType type, object log)
        {
            if(IsLogTypeAllowed(type))
            {
                LogHandler.Log(type, log.ToString());
            }
        }

        private bool IsLogTypeAllowed(ELogType type)
        {
            return LogEnabled && (type <= LogFilter);
        }
    }
}
