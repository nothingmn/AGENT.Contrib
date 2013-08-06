using System;
using System.IO.Ports;
using Microsoft.SPOT;

namespace AGENT.Contrib.Communication.Channels
{
    public class StringChannel : IChannel
    {
        public StringChannel()
        {
        }

        public object ConvertTo(byte[] input)
        {
            return new string(System.Text.Encoding.UTF8.GetChars(input));
        }

        public byte[] ConvertFrom(object data)
        {
            return System.Text.Encoding.UTF8.GetBytes((string) data);
        }
    }
}