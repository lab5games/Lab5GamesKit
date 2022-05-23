﻿
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

    public enum LogType
    {
        Log,
        Warning,
        Error,
        Exception,
        Assert
    }

    public enum LogFilter
    {
        System,
        Network,
        Game
    }

    public enum EaseTypes
    {
        Linear = 0,

        InSine = 1,
        OutSine = 2,
        InOutSine = 3,

        InQuad = 4,
        OutQuad = 5,
        InOutQuad = 6,

        InCubic = 7,
        OutCubic = 8,
        InOutCubic = 9,

        InQuart = 10,
        OutQuart = 11,
        InOutQuart = 12,

        InExpo = 13,
        OutExpo = 14,
        InOutExpo = 15,

        InCirc = 16,
        OutCirc = 17,
        InOutCirc = 18,

        InBack = 19,
        OutBack = 20,
        InOutBack = 21,

        InElastic = 22,
        OutElastic = 23,
        InOutElastic = 24,

        InBounce = 25,
        OutBounce = 26,
        InOutBounce = 27
    }
}
