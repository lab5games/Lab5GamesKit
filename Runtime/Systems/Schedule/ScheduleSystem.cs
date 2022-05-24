using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lab5Games
{
    public class ScheduleSystem : Singleton<ScheduleSystem>, ISystem
    {
        public bool showLog = true;

        public SystemStatus Status { get; private set; }
        
        public string Message { get; private set; }

        float _dt;

        List<Schedule> _scheduleList = new List<Schedule>();

        public void StopAll()
        {
            if(showLog)
                GLogger.LogToFilter("[ScheduleSystem] Stop all schedules.", LogFilter.System, this);

            foreach(var schedule in _scheduleList)
            {
                schedule.Cancel();
            }

            _scheduleList.Clear();
        }

        public void AddSchedule(Schedule newSchedule)
        {
            if(!_scheduleList.Contains(newSchedule))
            {
                _scheduleList.Add(newSchedule);

                if (newSchedule is RoutineSchedule)
                {
                    StartCoroutine((newSchedule as RoutineSchedule).Task());
                }
            }
        }

        void Start()
        {
            Message = "[ScheduleSystem] Successed.";

            if (showLog)
                GLogger.LogToFilter(Message, LogFilter.System, this);

            Status = SystemStatus.Success;
        }

        void Update()
        {
            _dt = Time.deltaTime;

            for(int i=_scheduleList.Count-1; i>=0; i--)
            {
                var schedule = _scheduleList[i];

                if (schedule.Status == ScheduleStatus.Running)
                {
                    schedule.Tick(_dt);
                }

                if(schedule.Status == ScheduleStatus.Canceled || schedule.Status == ScheduleStatus.Completed)
                {
                    _scheduleList.RemoveAt(i);
                }
            }
        }
    }
}
