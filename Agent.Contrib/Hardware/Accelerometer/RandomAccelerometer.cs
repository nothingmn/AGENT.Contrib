using System;
using System.Threading;
using Microsoft.SPOT;

namespace AGENT.Contrib.Hardware.Accelerometer
{
    public class RandomAccelerometer : IAccelerometer
    {
        private Timer randTimer = null;

        public RandomAccelerometer()
        {
            NewRandomReading();
            randTimer = new Timer(NewReading, null, 1000, 1000);
        }
        private void NewRandomReading()
        {
            CurrentReading = new Reading()
            {
                X = rnd.NextDouble()*10,
                Y = rnd.NextDouble()*10,
                Z = rnd.NextDouble()*10,
                TimeStamp = DateTime.Now
            };
        }
        System.Random rnd = new Random();
        private void NewReading(object state)
        {

            NewRandomReading();
            if (OnAccelerometerReading != null) OnAccelerometerReading(CurrentReading);
            randTimer.Change(1000, rnd.Next(3000));
        }
        public Reading CurrentReading { get; set; }
        public event AccelerometerReading OnAccelerometerReading;
    }
}
