using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.Notifications
{
    public class NotificationProvider
    {
        private static IProvideNotifications notificationProvider;
        private static object _notificationProviderLock = new object();
        public static IProvideNotifications Current
        {
            get
            {
                lock (_notificationProviderLock)
                {
                    notificationProvider = new SimpleNotifications();
                    return notificationProvider;
                }
            }
        }

        
    }
}
