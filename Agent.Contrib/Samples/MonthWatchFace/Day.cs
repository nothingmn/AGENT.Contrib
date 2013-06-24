using System;
using Agent.Contrib.Drawing;
using Microsoft.SPOT;

namespace MonthWatchFace
{
    public class Day
    {
        public DateTime Timestamp { get; set; }
        public Point Point { get; set; }
        public string Text { get; set; }
    }
}
