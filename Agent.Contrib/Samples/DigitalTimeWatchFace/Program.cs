using System;
using Agent.Contrib.Notifications;
using Agent.Contrib.Settings;
using Microsoft.SPOT;
using Agent.Contrib.Face;

namespace DigitalTimeWatchFace
{
    public class Program
    {
        public static void Main()
        {
            new WatchFace(new DigitalTimeFace( NotificationProvider.Current, SettingsProvider.Current)).Start();
        }

    }
}
