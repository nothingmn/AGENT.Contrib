using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Weather
{
    public class WeatherProvider
    {
        private static IWeatherForecast _weatherForecastProvider;
        private static object _weatherProviderLock = new object();
        public static IWeatherForecast Current
        {
            get
            {
                lock (_weatherProviderLock)
                {
                    _weatherForecastProvider = new SimpleWeatherForecast();
                    return _weatherForecastProvider;
                }
            }
        }
    }
}
