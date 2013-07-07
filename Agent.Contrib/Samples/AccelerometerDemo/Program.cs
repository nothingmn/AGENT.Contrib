using System;
using Agent.Contrib.Hardware.Accelerometer;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;

namespace AccelerometerDemo
{
    public class Program
    {
        static Bitmap _display;

        public static void Main()
        {
            // initialize display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            Render();

            AccelerometerProvider.Current.OnAccelerometerReading += Current_OnAccelerometerReading;

            // go to sleep; all further code should be timer-driven or event-driven
            Thread.Sleep(Timeout.Infinite);
        }

        static void Current_OnAccelerometerReading(Reading reading)
        {
            Render();
        }

        static Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);

        public static void Render()
        {
            // sample "hello world" code
            _display.Clear();
            var reading = "X:" + AccelerometerProvider.Current.CurrentReading.X.ToString("d");
            _display.DrawText(reading, fontNinaB, Color.White, 1, 10);
            reading = "Y:" + AccelerometerProvider.Current.CurrentReading.Y.ToString("d");
            _display.DrawText(reading, fontNinaB, Color.White, 1, 30);
            reading = "Z:" + AccelerometerProvider.Current.CurrentReading.Z.ToString("d");
            _display.DrawText(reading, fontNinaB, Color.White, 1, 50);
            _display.Flush();
            
        }

    }
}
