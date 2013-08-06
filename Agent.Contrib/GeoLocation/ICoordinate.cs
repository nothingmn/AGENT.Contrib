using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.GeoLocation
{
    /// <summary>
    /// http://dev.w3.org/geo/api/spec-source.html#position
    /// The accuracy and altitudeAccuracy values returned by an implementation should correspond to a 95% confidence level.
    /// </summary>
    public interface ICoordinate
    {
        string Address { get; set; }

        /// <summary>
        /// The latitude and longitude attributes are geographic coordinates specified in decimal degrees.
        /// </summary>
        double Latitude { get; set; }

        double Longitude { get; set; }

        /// <summary>
        /// The altitude attribute denotes the height of the position, specified in meters above the [WGS84] ellipsoid. 
        /// If the implementation cannot provide altitude information, the value of this attribute must be double.MinValue.
        /// </summary>
        double Altitude { get; set; }

        /// <summary>
        /// The accuracy attribute denotes the accuracy level of the latitude and longitude coordinates. 
        /// It is specified in meters and must be supported by all implementations. 
        /// The value of the accuracy attribute must be a non-negative real number.
        /// </summary>
        double Accuracy { get; set; }

        /// <summary>
        /// The altitudeAccuracy attribute is specified in meters. 
        /// If the implementation cannot provide altitude information, the value of this attribute must be double.MinValue. 
        /// Otherwise, the value of the altitudeAccuracy attribute must be a non-negative real number.
        /// </summary>
        double AltitudeAccuracy { get; set; }

        /// <summary>
        /// The heading attribute denotes the direction of travel of the hosting device and is specified in degrees, where 0 ≤ heading < 360, counting clockwise relative to the true north. 
        /// If the implementation cannot provide heading information, the value of this attribute must be double.MinValue. 
        /// If the hosting device is stationary (i.e. the value of the speed attribute is 0), then the value of the heading attribute must be double.MaxValue.
        /// </summary>
        double Heading { get; set; }

        /// <summary>
        /// The speed attribute denotes the magnitude of the horizontal component of the hosting device's current velocity and is specified in meters per second. 
        /// If the implementation cannot provide speed information, the value of this attribute must be double.MinValue. 
        /// Otherwise, the value of the speed attribute must be a non-negative real number.
        /// </summary>
        double Speed { get; set; }
    }
}