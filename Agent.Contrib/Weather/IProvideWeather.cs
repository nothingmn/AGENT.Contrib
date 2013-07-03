using System;
using System.Collections;
using Microsoft.SPOT;

namespace Agent.Contrib.Weather
{
    public interface IProvideWeather
    {
        IForecast CurrentForecast { get; }
        ArrayList CurrentWeekForecast { get; }
    }
}
