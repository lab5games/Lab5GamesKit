using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lab5Games
{
    public class ScheduleSystem : ComponentSingleton<ScheduleSystem>
    {
        public override bool IsPersistent => true;

        public bool showLog = true;

        float _dt;

        List<Schedule> _scheduleList = new List<Schedule>();

        public void StopAll()
        {
            if(showLog)
                GLogger.LogToFilter("[ScheduleSystem] Stop all schedules.", GLogFilter.System, this);

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

                if (newSchedule is CoroutineSchedule)
                {
                    StartCoroutine((newSchedule as CoroutineSchedule).Task());
                }
            }
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
