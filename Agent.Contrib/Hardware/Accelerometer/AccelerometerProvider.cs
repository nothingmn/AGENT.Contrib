using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware.Accelerometer
{
    public delegate void AccelerometerReading(Reading reading);

    public class AccelerometerProvider
    {

        private static object _lock = new object();
        private static IAccelerometer _acc;
        public static IAccelerometer Current
        {
            get
            {
                lock (_lock)
                {
                    if(_acc == null) _acc = new RandomAccelerometer();
                    return _acc;
                }
            }
        }
    }
}
