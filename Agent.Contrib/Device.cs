using System;
using AGENT.Contrib.Drawing;
using AGENT.Contrib.Settings;
using Microsoft.SPOT;

namespace AGENT.Contrib
{
    public class Device
    {
        
        public static int Size = 128;
        public static Point Center = new Drawing.Point(64, 64);

        public static TimeSpan UpTime
        {
            get { return Microsoft.SPOT.Hardware.PowerState.Uptime; }
        }

        public static System.Version Version
        {
            get { return Microsoft.SPOT.Hardware.SystemInfo.Version; }
        }

        public static string OEM
        {
            get { return Microsoft.SPOT.Hardware.SystemInfo.OEMString; }
        }

        public static ISettings Settings
        {
            get { return SettingsProvider.Current; }
        }

  
    }
}