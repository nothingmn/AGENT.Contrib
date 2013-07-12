using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware.Accelerometer
{
    public class Reading
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
