using System;
using System.IO.Ports;
using Microsoft.SPOT;

namespace Agent.Contrib.Communication.Channels
{
    public abstract class BaseChannel : IChannel
    {
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

        //protected string GetString(System.IO.Ports.SerialPort port)
        //{
        //    var ret = string.Empty;

        //    var latestBype = port.ReadByte();

        //    //Keep getting data until the latest byte is a zero byte
        //    while (latestBype != 0)
        //    {
        //        var character = (char)latestBype;

        //        ret += character;

        //        latestBype = port.ReadByte();
        //    }
        //    return ret;
        //}
        /// <summary>
        /// Get a  byte[] from the serial port.
        /// </summary>
        /// <returns>The byte[] that has been sent by the other application</returns>
        protected byte[] GetBytes(SerialPort port)
        {

            int position = 0;
            byte[] buffer = new byte[255];
            var latestByte = port.ReadByte();
            buffer[position] = (byte)latestByte;
            
            //Keep getting data until the latest byte is a zero byte
            while (latestByte != 0)
            {
                latestByte  = port.ReadByte();
                position++;
                buffer[position] = (byte)latestByte;

            }
            return buffer;
        }

        public abstract object Read(System.IO.Ports.SerialPort port);
    }
}
