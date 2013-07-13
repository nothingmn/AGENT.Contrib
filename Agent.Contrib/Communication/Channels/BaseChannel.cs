using System;
using System.IO.Ports;
using Microsoft.SPOT;

namespace Agent.Contrib.Communication.Channels
{
    public abstract class BaseChannel : IChannel
    {
        public abstract object Read(System.IO.Ports.SerialPort port);
        public abstract void Write(SerialPort port, object Data);
        /// <summary>
        /// Get a string from the serial port.
        /// NOTE: This may not be the most efficient way of receiving strings from the serial port.
        /// </summary>
        /// <returns>The string that has been sent by the other application</returns>
        protected string GetString(System.IO.Ports.SerialPort port)
        {
            byte[] data = GetBytes(port);
            return new string(System.Text.Encoding.UTF8.GetChars(data));
        }

        /// <summary>
        /// Get a  byte[] from the serial port.
        /// </summary>
        /// <returns>The byte[] that has been sent by the other application</returns>
        protected byte[] GetBytes(SerialPort port)
        {
            byte[] buffer = new byte[port.BytesToRead];
            port.Read(buffer, 0, buffer.Length);
            return buffer;
        }

    }
}
