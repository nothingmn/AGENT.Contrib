using System;
using System.IO.Ports;
using Agent.Contrib.Communication.Channels;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware.Bluetooth
{

    public delegate void Received(object Data, SerialPort port, IChannel channel, DateTime Timestamp);

    public class Connection : IDisposable
    {
        public event Received OnReceived;

        private SerialPort _port;
        private IChannel _channel;

        public Connection(string Port, IChannel channel, int baudRate = 9600, Parity parity = Parity.None,
                          int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            _port = new SerialPort(Port, baudRate, parity, dataBits, stopBits);
            _port.DataReceived += p_DataReceived;
            _channel = channel;
            

        }

        public void Open()
        {
            _port.Open();
        }
        public void Close()
        {
            _port.Close();
        }

        public bool IsOpen
        {
            get { return (_port != null && _port.IsOpen); }
        }

        public void Write(object Data)
        {
            _channel.Write(_port, Data);
        }

        private void p_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var result = _channel.Read(_port);
            if (OnReceived != null) OnReceived(result, _port, _channel, DateTime.Now);
        }

        public void Dispose()
        {
            if (_port != null)
            {
                if (_port.IsOpen) _port.Close();
                _port.Dispose();
            }
        }
    }
}