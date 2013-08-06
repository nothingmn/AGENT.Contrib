using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.Notifications
{
    public delegate void NotificationReceived(INotification notification);

    public class Notification : INotification
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
