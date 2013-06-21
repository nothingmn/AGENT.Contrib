using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Communication
{
    public interface IChannel
    {
        string Name { get; set; }
        bool Connect();
        byte[] Receive();
        void Send(byte[] data);
        bool IsOpen { get; set; }

    }
}
