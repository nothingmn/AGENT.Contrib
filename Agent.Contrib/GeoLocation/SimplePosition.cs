using System;
using Microsoft.SPOT;

namespace Agent.Contrib.GeoLocation
{
    public class SimplePosition : IPosition
    {
        public ICoordinate Coordinates { get; set; }
        public DateTime TimeStamp { get; set; }

        public SimplePosition()
        {
            TimeStamp = Settings.SettingsProvider.Current.Now;
            Coordinates = new Coordinate()
                {
                    Accuracy = 5,
                    Address = "101 Main Street, Vancouver BC Canada",
                    Altitude = 23,
                    AltitudeAccuracy = 5,
                    Heading = 1,
                    Latitude = 49.903,
                    Longitude = -122.34,
                    Speed = 0
                };
        }
    }
}