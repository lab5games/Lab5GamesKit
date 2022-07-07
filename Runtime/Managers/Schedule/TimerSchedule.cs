using System;

namespace Lab5Games
{
    public class TimerSchedule : Schedule, IAwaitable<TimerSchedule>, IAwaiter
    {
        public static TimerSchedule Create(float time, bool autoStart = true)
        {
            TimerSchedule schedule = new TimerSchedule(time);

            if (autoStart)
                schedule.Start();

            return schedule;
        }

        float _time;
        
        public TimerSchedule(float time)
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

        public TimerSchedule GetAwaiter()
        {
            return this;
        }
    }
}
