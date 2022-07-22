using UnityEngine;

namespace Lab5Games
{
    [System.Serializable]
    public struct BooleanAction
    {
        public bool value;

        /// <summary>
        /// Returns true if the value transitioned from false to true
        /// </summary>
        public bool Started { get; private set; }
        /// <summary>
        /// Returns true if the value transitioned from true to false
        /// </summary>
        public bool Canceled { get; private set; }
        /// <summary>
        /// Elpased time since the last "Started" flag
        /// </summary>
        public float StartedElpasedTime { get; private set; }
        /// <summary>
        /// Elpased time since the last "Canceled" flag
        /// </summary>
        public float CanceledElpasedTime { get; private set; }
        /// <summary>
        /// The elpased time since this action was set to true
        /// </summary>
        public float ActiveTime { get; private set; }
        /// <summary>
        /// The elpased time since this action was set to false
        /// </summary>
        public float InactiveTime { get; private set; }
        /// <summary>
        /// The last "ActiveTime" value registered by this action (on Canceled).
        /// </summary>
        public float LastActiveTime { get; private set; }
        /// <summary>
        /// The last "InactiveTime" value registered by this action (on Started).
        /// </summary>
        public float LastInactiveTime { get; private set; }


        bool previouseValue;
        bool previouseStarted;
        bool previouseCanceled;

        public static implicit operator bool(BooleanAction action) => action.value;

        public void Reset()
        {
            value = false;
            Started = false;
            Canceled = false;

            StartedElpasedTime = Mathf.Infinity;
            CanceledElpasedTime = Mathf.Infinity;

            ActiveTime = 0;
            InactiveTime = 0;
            LastActiveTime = 0;
            LastInactiveTime = 0;

            previouseValue = false;
            previouseCanceled = false;
            previouseStarted = false;
        }

        public void Tick(float dt)
        {
            Started = !previouseValue && value;
            Canceled = previouseValue && !value;

            StartedElpasedTime += dt;
            CanceledElpasedTime += dt;

            if (Started)
            {
                StartedElpasedTime = 0f;

                if (!previouseStarted)
                {
                    LastActiveTime = 0;
                    LastInactiveTime = InactiveTime;
                }
            }

            if (Canceled)
            {
                CanceledElpasedTime = 0f;

                if (!previouseCanceled)
                {
                    LastActiveTime = ActiveTime;
                    LastInactiveTime = 0f;
                }
            }

            if (value)
            {
                ActiveTime += dt;
                InactiveTime = 0f;
            }
            else
            {
                ActiveTime = 0;
                InactiveTime += dt;
            }

            previouseValue = value;
            previouseStarted = Started;
            previouseCanceled = Canceled;
        }
    }
}
