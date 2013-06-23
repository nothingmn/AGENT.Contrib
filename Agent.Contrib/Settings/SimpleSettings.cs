using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Settings
{
    public class SimpleSettings : ISettings
    {
        public SimpleSettings()
        {
            TimeInISONotatation = true;
            DST = true;
        }

        public bool TimeInISONotatation { get; set; }
        public bool DST { get; set; }

        public DateTime Now
        {
            get
            {
                var now = DateTime.Now;
                if (DST) now = now.AddHours(1);
                return now;
            }
        }
    }
}