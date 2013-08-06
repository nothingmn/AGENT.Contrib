using System;
using System.Threading;
using Microsoft.SPOT;

namespace AGENT.Contrib.GeoLocation
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

            geoTimer = new Timer(updatePosition, null, 5000, 5000);
        }

        private System.Threading.Timer geoTimer;
        private void updatePosition(object state)
        {
            Coordinates.Latitude += 0.5;
            Coordinates.Longitude += 0.5;
            if (OnPositionUpdated != null) OnPositionUpdated(this);
        }

        public event PositionUpdated OnPositionUpdated;
    }
}