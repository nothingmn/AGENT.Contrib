using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.Hardware.Bluetooth
{
    public interface IConnection
    {
        event Received OnReceived;
        bool IsOpen { get; }
        void Open();
        void Close();
        void Write(object Data);
        void Dispose();
    }

}
