using System;
using Agent.Contrib.Face;
using Agent.Contrib.Hardware;
using Agent.Contrib.Notifications;
using Agent.Contrib.Settings;
using Microsoft.SPOT;

namespace MonthWatchFace
{
    public class Program
    {

        public static void Main()
        {

            var watch = new WatchFace(new MonthFace(NotificationProvider.Current, SettingsProvider.Current));
            watch.Start();
        }

    }
}
