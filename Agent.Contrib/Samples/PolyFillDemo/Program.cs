using System;
using Agent.Contrib.Face;
using Agent.Contrib.Hardware;
using Agent.Contrib.Notifications;
using Agent.Contrib.Settings;
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
