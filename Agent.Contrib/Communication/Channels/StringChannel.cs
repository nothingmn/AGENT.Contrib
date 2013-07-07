using System;
using System.IO.Ports;
using Microsoft.SPOT;

namespace Agent.Contrib.Communication.Channels
{
    public class StringChannel : BaseChannel
    {

        public override object Read(System.IO.Ports.SerialPort port)
        {
            string result = (base.GetString(port) as string);
            return result;

        }


        public override void Write(SerialPort port, object Data)
        {
            string data = (Data as string);
            byte[] payload = System.Text.Encoding.UTF8.GetBytes(data);
            port.Write(payload, 0, payload.Length);
        }
    }
}
