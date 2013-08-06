using System;
using System.Collections;
using Microsoft.SPOT;

namespace AGENT.Contrib.Notifications
{
    public interface IProvideNotifications
    {
        event NotificationReceived OnNotificationReceived;
        ArrayList Notifications { get; }        
    }
}
