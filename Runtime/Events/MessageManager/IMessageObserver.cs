using System;

namespace Lab5Games.Events
{
    public interface IMessageObserver
    {
        void OnMessage(int messageType, object param);
    }
}
