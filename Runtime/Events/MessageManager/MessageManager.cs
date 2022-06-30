using System;
using System.Collections.Generic;

namespace Lab5Games.Events
{
    public static class MessageManager
    {
        static Dictionary<int, List<IMessageObserver>> _messageTable = new Dictionary<int, List<IMessageObserver>>();

        public static void RegisterObserver(int messageType, IMessageObserver observer)
        {
            List<IMessageObserver> list = null;

            if(!_messageTable.TryGetValue(messageType, out list))
            {
                list = new List<IMessageObserver>();
            }

            list.Add(observer);
        }

        public static void UnregisterObserver(int messageType, IMessageObserver observer)
        {
            if (_messageTable.TryGetValue(messageType, out List<IMessageObserver> list))
            {
                list.Remove(observer);
            }
        }

        public static void Post(int messageType, object param = null)
        {
            if (_messageTable.TryGetValue(messageType, out List<IMessageObserver> list))
            {
                foreach(var observer in list)
                {
                    observer.OnMessage(messageType, param);
                }
            }
        }

        public static void Clear(int messageType)
        {
            _messageTable.Remove(messageType);
        }

        public static void Clera()
        {
            _messageTable.Clear();
        }
    }
}
