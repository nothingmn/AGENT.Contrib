using System;
using Agent.Contrib.GeoLocation;
using Microsoft.SPOT;

namespace Agent.Contrib.Weather
{
    public interface IForecast
    {
        IPosition Position { get; set; }
        double Temperature { get; set; }
        double ChanceOfPrecipitation { get; set; }
        string Details { get; set; }
        DateTime Date { get; set; }
        string TimeName { get; set; }
        bool IsCelsius { get; set; }
    }
}
