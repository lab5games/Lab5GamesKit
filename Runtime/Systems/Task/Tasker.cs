using System;

namespace Lab5Games
{ 
    public abstract class Tasker
    {
        public TaskStatus Status { get; protected set; }

        public abstract void Tick(float dt);

        public Action onCompleted;
        public Action onCanceled;

        public virtual void Start()
        {
            if(Status == TaskStatus.Ready)
            {
                Status = TaskStatus.Running;
            }
        }

        public void Complete()
        {
            if(Status == TaskStatus.Running || Status == TaskStatus.Paused)
                Status = TaskStatus.Completed;
        }

        public void Cancel()
        {
            if(Status == TaskStatus.Running || Status == TaskStatus.Paused)
                Status = TaskStatus.Canceled;
        }

        public void Pause()
        {
            if(Status == TaskStatus.Running)
                Status = TaskStatus.Paused;
        }

        public void Unpause()
        {
            if(Status == TaskStatus.Paused)
                Status = TaskStatus.Running;
        }
    }
}
