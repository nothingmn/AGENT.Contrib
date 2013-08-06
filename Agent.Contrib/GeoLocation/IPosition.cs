using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.GeoLocation
{
    public interface IPosition
    {
        ICoordinate Coordinates { get; set; }
        DateTime TimeStamp { get; set; }

        event PositionUpdated OnPositionUpdated;
    }
}
