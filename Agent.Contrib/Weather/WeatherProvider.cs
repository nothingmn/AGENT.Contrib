using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Weather
{
    public class WeatherProvider
    {
        private static IProvideWeather weatherProvider;
        private static object _weatherProviderLock = new object();
        public static IProvideWeather Current
        {
            get
            {
                lock (_weatherProviderLock)
                {
                    weatherProvider = new SimpleWeather();
                    return weatherProvider;
                }
            }
        }
    }
}
