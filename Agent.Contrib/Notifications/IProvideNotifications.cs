using System;
using System.Collections;
using Microsoft.SPOT;

namespace Agent.Contrib.Notifications
{
    public interface IProvideNotifications
    {
        event NotificationReceived OnNotificationReceived;
        ArrayList Notifications { get; }        
    }
}
