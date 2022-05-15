
namespace Lab5Games
{

    public enum GameSystemStatus
    {
        None,
        Success,
        Failure
    }

    public enum TaskStatus
    {
        Ready,
        Running,
        Paused,
        Completed,
        Canceled
    }

    public enum SoundType
    {
        Music,
        Effect,
        UI
    }

    internal enum LogType
    {
        Log,
        Warning,
        Error,
        Exception,
        Assert
    }

    internal enum LogFilter
    {
        System,
        Network,
        Game
    }
}
