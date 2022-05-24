using System;

namespace Lab5Games
{
    public class TimeSchedule : Schedule, IAwaitable<TimeSchedule>, IAwaiter
    {
        public static TimeSchedule Create(float time, bool autoStart = true)
        {
            TimeSchedule schedule = new TimeSchedule(time);

            if (autoStart)
                schedule.Start();

            return schedule;
        }

        float _time;
        
        public TimeSchedule(float time)
        {
            _time = time;
        }

        public override void Tick(float dt)
        {
            _time -= dt;

            if(_time <= 0)
            {
                Complete();
            }
        }

        public bool IsCompleted
        {
            get
            {
                return Status == ScheduleStatus.Completed || Status == ScheduleStatus.Canceled;
            }
        }

        public void GetResult()
        {
            
        }

        public void OnCompleted(Action continuation)
        {
            onCompleted += continuation;
            onCanceled += continuation;
        }

        public TimeSchedule GetAwaiter()
        {
            return this;
        }
    }
}
