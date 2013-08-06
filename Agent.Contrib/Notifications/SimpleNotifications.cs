using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;

namespace AGENT.Contrib.Notifications
{
    public class SimpleNotifications : IProvideNotifications
    {
        public SimpleNotifications()
        {
            RandomNotification();
        }

        public event NotificationReceived OnNotificationReceived;
        private ArrayList _notifications;
        private static object _lock = new object();

        public System.Collections.ArrayList Notifications
        {
            get
            {
                lock (_lock)
                {
                    if (_notifications == null)
                    {
                        _notifications = new ArrayList();
                    }
                }
                return _notifications;
            }
        }

        private INotification GetNewRandomNotification()
        {
            var names = new string[] {"John", "Mary", "Paul", "Eric", "Rob"};
            var types = new string[] {"Email", "Text", "Voice", "Calendar"};
            var type = types[Util.Random.Next(0, names.Length - 1)];
            return new Notification()
                {
                    Text = "New " + type + " from " + names[Util.Random.Next(0, names.Length - 1)],
                    TimeStamp = Settings.SettingsProvider.Current.Now,
                    Type = type
                };
        }

        private Timer timer;

        private void RandomNotification()
        {
            timer = new Timer(state =>
                {
                    var n = GetNewRandomNotification();

                    Notifications.Add(n);

                    Debug.Print("new notification:" + n.Text);
                    if (OnNotificationReceived != null) OnNotificationReceived(n);
                }, null, 5000, Util.Random.Next(1000, 10000));
        }

        private Random rnd = new Random();
    }
}