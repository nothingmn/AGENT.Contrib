using System;
using Microsoft.SPOT;

namespace Agent.Contrib.GeoLocation
{
    public class Coordinate : ICoordinate
    {
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Accuracy { get; set; }
        public double AltitudeAccuracy { get; set; }
        public double Heading { get; set; }
        public double Speed { get; set; }
    }
}
