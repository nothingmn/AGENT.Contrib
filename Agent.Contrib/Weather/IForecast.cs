using System;
using AGENT.Contrib.GeoLocation;
using Microsoft.SPOT;

namespace AGENT.Contrib.Weather
{
    public interface IForecast
    {
        IPosition Position { get; set; }
        double Temperature { get; set; }
        double ChanceOfPrecipitation { get; set; }
        string Details { get; set; }
        DateTime TimeStamp { get; set; }
        string TimeName { get; set; }
        bool IsCelsius { get; set; }
    }
}
