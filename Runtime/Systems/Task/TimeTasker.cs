using System;

namespace Lab5Games
{
    public class TimeTasker : Tasker, IAwaitable<TimeTasker>, IAwaiter
    {
        public static TimeTasker Create(float seconds, bool autoStart = true)
        {
            TimeTasker newTasker = new TimeTasker(seconds);

            if (autoStart)
                newTasker.Start();

            return newTasker;
        }

        float _remaining;

        public float Remaining => _remaining;

        private TimeTasker(float seconds)
        {
            _remaining = seconds;
        }

        public override void Tick(float dt)
        {
            _remaining -= dt;

            if(_remaining <= 0)
            {
                _remaining = 0;
                Complete();
            }
        }

        public bool IsCompleted
        {
            get
            {
                return Status == TaskStatus.Completed || Status == TaskStatus.Canceled;
            }
        }

        public void GetResult()
        {

        }

        public void OnCompleted(Action continuation)
        {
            onCompleted += () => continuation?.Invoke();
            onCanceled += () => continuation?.Invoke();
        }

        public TimeTasker GetAwaiter()
        {
            return this;
        }
    }
}
