using System;
using System.Collections;

namespace Lab5Games
{
    public class RoutineSchedule : Schedule, IAwaitable<RoutineSchedule>, IAwaiter
    {
        public static RoutineSchedule Create(IEnumerator task, bool autoStart = true)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            RoutineSchedule schedule = new RoutineSchedule(task);

            if (autoStart)
                schedule.Start();

            return schedule;
        }

        IEnumerator _task;
        bool _running = false;

        private RoutineSchedule(IEnumerator task)
        {
            _task = task;
        }

        public override void Tick(float dt)
        {
            
        }

        internal IEnumerator Task()
        {
            _running = true;

            while(_running)
            {
                if(Status == ScheduleStatus.Paused)
                {
                    yield return Yielders.EndOfFrame;
                }
                else
                {
                    if(Status == ScheduleStatus.Canceled)
                    {
                        _running = false;
                    }
                    else
                    {
                        if(_task.MoveNext())
                        {
                            yield return _task.Current;
                        }
                        else
                        {
                            _running = false;
                            Complete();
                        }
                    }
                }
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

        public RoutineSchedule GetAwaiter()
        {
            return this;
        }
    }
}
