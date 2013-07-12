using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware.Accelerometer
{
    public interface IAccelerometer
    {
        Reading CurrentReading { get; set; }
        event AccelerometerReading OnAccelerometerReading;
    }
}
