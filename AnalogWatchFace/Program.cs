using System;
using Agent.Contrib.Face;
using Agent.Contrib.Notifications;
using Microsoft.SPOT;

namespace AnalogWatchFace
{
    public class Program
    {
        public static void Main()
        {
            new WatchFace(new AnalogFace(NotificationProvider.Current)).Start();
        }

    }
}
