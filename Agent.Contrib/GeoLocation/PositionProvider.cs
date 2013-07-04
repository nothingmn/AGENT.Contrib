using System;
using Microsoft.SPOT;

namespace Agent.Contrib.GeoLocation
{
    public delegate void PositionUpdated(IPosition newPosition);
    public class PositionProvider
    {
        private static IPosition position;
        private static object _positionLock = new object();
        public static IPosition Current
        {
            get
            {
                lock (_positionLock)
                {
                    position = new SimplePosition();
                    return position;
                }
            }
        }
    }
}
