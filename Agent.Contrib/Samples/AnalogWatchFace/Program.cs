using System;
using AGENT.Contrib.Face;
using AGENT.Contrib.Notifications;
using AGENT.Contrib.Settings;
using Microsoft.SPOT;

namespace AnalogWatchFace
{
    public class Program
    {
        public static void Main()
        {
            new WatchFace(new AnalogFace(NotificationProvider.Current, SettingsProvider.Current)).Start();
        }

    }
}
