using System;
using UnityEngine;

namespace Lab5Games
{
    public static class Yielders
    {
        readonly static WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        readonly static WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

        public static WaitForFixedUpdate FixedUpdate
        {
            get { return _waitForFixedUpdate; }
        }

        public static WaitForEndOfFrame EndOfFrame
        {
            get { return _waitForEndOfFrame; }
        }

        public static WaitForSeconds Seconds(float seconds)
        {
            return new WaitForSeconds(seconds);
        }

        public static WaitForSecondsRealtime SecondsRealtime(float seconds)
        {
            return new WaitForSecondsRealtime(seconds);
        }

        public static WaitUntil Until(Func<bool> predicate)
        {
            return new WaitUntil(predicate);
        }

        public static WaitWhile While(Func<bool> predicate)
        {
            return new WaitWhile(predicate);
        }
    }
}
