using System;
using System.IO.Ports;
using Microsoft.SPOT;

namespace AGENT.Contrib.Communication.Channels
{
    /// <summary>
    /// Responsible for converting raw inputs to object outputs
    /// Also from object inputs to raw outputs
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// Convert to actual, from raw
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        object ConvertTo(byte[] input);

        /// <summary>
        /// Convert from actual to raw
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] ConvertFrom(object data);
    }
}
