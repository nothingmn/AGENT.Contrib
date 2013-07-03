using System;
using Agent.Contrib.Face;
using Agent.Contrib.Weather;
using Microsoft.SPOT;

namespace WeatherFace
{
    public class Program
    {
        public static void Main()
        {
            WeatherWatchFace face = new WeatherWatchFace();
            face.WeatherProvider = WeatherProvider.Current;
            var watch = new WatchFace(face);
            watch.Start();
        }

    }
}
