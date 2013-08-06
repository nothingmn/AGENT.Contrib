using System;
using AGENT.Contrib.Notifications;
using AGENT.Contrib.Settings;
using Microsoft.SPOT;
using AGENT.Contrib.Face;

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
