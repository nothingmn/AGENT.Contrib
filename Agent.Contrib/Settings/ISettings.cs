using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Settings
{
    public interface ISettings
    {
        /// <summary>
        /// To use a 12 or 24 hour clock.
        /// true is a 24 hour, false is 12 hour
        /// </summary>
        bool TimeInISONotatation { get; set; }

        /// <summary>
        /// If the watch is in a location which shifted for DST
        /// </summary>
        bool DST { get; set; }

        DateTime Now { get; }
    }
}
