using System;
using System.Collections;

namespace Lab5Games
{
    public class CoroutineTasker : Tasker, IAwaitable<CoroutineTasker>, IAwaiter
    {
        public static CoroutineTasker Create(IEnumerator task, bool autoStart = true)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            CoroutineTasker newTasker = new CoroutineTasker(task);

            if (autoStart)
                newTasker.Start();

            return newTasker;
        }


        IEnumerator _task;

        private CoroutineTasker(IEnumerator task)
        {
            _task = task;
        }

        public override void Tick(float dt)
        {
            
        }

        internal IEnumerator Task()
        {
            bool running = true;

            while(running)
            {
                if(Status == TaskStatus.Paused)
                {
                    yield return Yielders.EndOfFrame;
                }
                else
                {
                    if(Status == TaskStatus.Canceled)
                    {
                        running = false;
                    }
                    else
                    {
                        if(_task.MoveNext())
                        {
                            yield return _task.Current;
                        }
                        else
                        {
                            running = false;
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

        public CoroutineTasker GetAwaiter()
        {
            return this;
        }
    }
}
