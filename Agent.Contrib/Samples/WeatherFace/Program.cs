using System;
using AGENT.Contrib.Face;
using AGENT.Contrib.Weather;
using Microsoft.SPOT;

namespace WeatherFace
{
    public class Program
    {
        public static void Main()
        {
            var watch = new WatchFace(new WeatherWatchFace());
            watch.Start();
        }

    }
}
