using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.Notifications
{
    public class NotificationSummary
    {
        public NotificationSummary(IProvideNotifications notifications)
        {
            if (notifications != null)
            {
                foreach (INotification n in notifications.Notifications)
                {
                    if (n.Type == "Email") EmailCount++;
                    if (n.Type == "Text") TextCount++;
                    if (n.Type == "MissedCall") MissedCallCount++;
                    if (n.Type == "Voice") VoiceCount++;

                }
            }
        }

        public int EmailCount { get; set; }
        public int TextCount { get; set; }
        public int VoiceCount { get; set; }
        public int MissedCallCount { get; set; }
    }
}
