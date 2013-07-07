using System;
using System.IO.Ports;
using Microsoft.SPOT;

namespace Agent.Contrib.Communication.Channels
{
    public class ByteChannel : BaseChannel
    {

        public override object Read(System.IO.Ports.SerialPort port)
        {
            return base.GetBytes(port);
        }


        public override void Write(SerialPort port, object Data)
        {
            byte[] data = (Data as byte[]);
            port.Write(data, 0, data.Length);
        }
    }
}
