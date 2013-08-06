using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.Hardware.Accelerometer
{
    public interface IAccelerometer
    {
        Reading CurrentReading { get; set; }
        event AccelerometerReading OnAccelerometerReading;
    }
}
