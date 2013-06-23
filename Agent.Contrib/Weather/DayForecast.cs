using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Weather
{
    public class DayForecast : IForecast
    {
        public GeoLocation.IPosition Position { get; set; }
        public double Temperature { get; set; }
        public double ChanceOfPrecipitation { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public string TimeName { get; set; }
        public bool IsCelsius { get; set; }
    }
}
