using System;
using Microsoft.SPOT;

namespace Agent.Contrib.GeoLocation
{
    public interface IPosition
    {
        ICoordinate Coordinates { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
