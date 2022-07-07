using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lab5Games
{
    public class ScheduleManager : ComponentSingleton<ScheduleManager>
    {
        public override bool IsPersistent => false;

        float _dt;

        List<Schedule> _scheduleList = new List<Schedule>();
        List<LevelOperation> _levelOperationList = new List<LevelOperation>();

        public void StopAll()
        {
            GLogger.LogToFilter("[ScheduleManager] Stop all schedules.", GLogFilter.System, this);


            foreach (var schedule in _scheduleList)
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

        internal void AddLevelOperation(LevelOperation levelOp)
        {
            _levelOperationList.Add(levelOp);
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

        private void LateUpdate()
        {
            _dt = Time.deltaTime;

            for(int i=_levelOperationList.Count-1; i>=0; i--)
            {
                var levelOp = _levelOperationList[i];

                if(levelOp.Status == ScheduleStatus.Running)
                {
                    levelOp.Tick(_dt);
                }

                if(levelOp.Status == ScheduleStatus.Canceled ||
                    levelOp.Status == ScheduleStatus.Completed)
                {
                    _levelOperationList.RemoveAt(i);
                }
            }
        }
    }
}
