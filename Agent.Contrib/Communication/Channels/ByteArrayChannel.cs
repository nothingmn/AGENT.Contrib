using System;
using System.IO.Ports;
using Microsoft.SPOT;

namespace AGENT.Contrib.Communication.Channels
{
    public class ByteArrayChannel : IChannel
    {

        public object ConvertTo(byte[] input)
        {
            return (object)input;
        }

        public byte[] ConvertFrom(object data)
        {
            return (byte[])data;
        }
    }
}
