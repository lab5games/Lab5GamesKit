using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lab5Games
{
    public class TaskSystem : Singleton<TaskSystem>, IGameSystem
    {
        public GameSystemStatus Status { get; private set; }
        
        public string Message { get; private set; }

        float _dt;

        List<Tasker> _taskerList = new List<Tasker>();

        public void AddTask(Tasker newTasker)
        {
            if(!_taskerList.Contains(newTasker))
            {
                _taskerList.Add(newTasker);

                if (newTasker is CoroutineTasker)
                    StartCoroutine((newTasker as CoroutineTasker).Task());
            }
        }

        void Start()
        {
            Status = GameSystemStatus.Success;
        }

        void Update()
        {
            _dt = Time.deltaTime;

            for(int i=_taskerList.Count-1; i>=0; i--)
            {
                var tasker = _taskerList[i];

                if (tasker.Status == TaskStatus.Running)
                {
                    tasker.Tick(_dt);
                }

                if(tasker.Status == TaskStatus.Completed)
                {
                    _taskerList.RemoveAt(i);
                    tasker.onCompleted?.Invoke();

                    continue;
                }

                if(tasker.Status == TaskStatus.Canceled)
                {
                    _taskerList.RemoveAt(i);
                    tasker.onCanceled?.Invoke();

                    continue;
                }
            }
        }
    }
}
