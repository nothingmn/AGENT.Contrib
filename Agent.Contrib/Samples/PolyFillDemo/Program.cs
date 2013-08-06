using System;
using AGENT.Contrib.Face;
using AGENT.Contrib.Hardware;
using AGENT.Contrib.Notifications;
using AGENT.Contrib.Settings;
using Microsoft.SPOT;

namespace PolyFillDemo
{
    public class Program
    {
        public static void Main()
        {

            var watch = new WatchFace(new PolyFillFace(NotificationProvider.Current, SettingsProvider.Current));
            watch.Start();
        }

    }
}
