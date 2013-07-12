using System;
using System.IO.Ports;
using Microsoft.SPOT;

namespace Agent.Contrib.Communication.Channels
{
    public interface IChannel
    {
        object Read(SerialPort port);
        void Write(SerialPort port, object data);
    }
}
