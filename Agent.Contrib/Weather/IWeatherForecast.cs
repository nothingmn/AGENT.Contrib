using System;
using System.Collections;
using Microsoft.SPOT;

namespace AGENT.Contrib.Weather
{
    public interface IWeatherForecast
    {
        event WeatherUpdated OnWeatherUpdated;
        IForecast CurrentForecast { get; }
        ArrayList CurrentWeekForecast { get; }
    }
}
