using System;

namespace Lab5Games
{ 
    public interface ISchedule
    {
        public ScheduleStatus Status { get; }

        void Start();
        void Complete();
        void Cancel();
        void Pause();
        void Unpause();
        void Tick(float dt);
    }

    public abstract class Schedule : ISchedule
    {
        public ScheduleStatus Status { get; protected set; }

        public abstract void Tick(float dt);

        public event Action onCompleted;
        public event Action onCanceled;

        public virtual void Start()
        {
            if(Status == ScheduleStatus.Ready)
            {
                Status = ScheduleStatus.Running;

                ScheduleSystem.Instance.AddSchedule(this);
            }
        }

        public void Complete()
        {
            if (Status == ScheduleStatus.Running || Status == ScheduleStatus.Paused)
            {
                Status = ScheduleStatus.Completed;

                onCompleted?.Invoke();
            }
        }

        public void Cancel()
        {
            if (Status == ScheduleStatus.Running || Status == ScheduleStatus.Paused)
            {
                Status = ScheduleStatus.Canceled;

                onCanceled?.Invoke();
            }
        }

        public void Pause()
        {
            if(Status == ScheduleStatus.Running)
                Status = ScheduleStatus.Paused;
        }

        public void Unpause()
        {
            if(Status == ScheduleStatus.Paused)
                Status = ScheduleStatus.Running;
        }
    }
}
