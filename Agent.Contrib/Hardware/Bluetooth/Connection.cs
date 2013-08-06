using System;
using System.IO.Ports;
using System.Threading;
using AGENT.Contrib.Communication.Channels;
using AGENT.Contrib.Util;
using Microsoft.SPOT;

namespace AGENT.Contrib.Hardware.Bluetooth
{

    public delegate void Received(object data, SerialPort port, IChannel channel, DateTime timestamp);


    public class Connection : IDisposable, IConnection
    {
        public event Received OnReceived;

        private SerialPort _port;
        private IChannel _channel;

        public Connection(string port, IChannel channel, int baudRate = 9600, Parity parity = Parity.None,
                          int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            _port = new SerialPort(port, baudRate, parity, dataBits, stopBits);
            _port.DataReceived += _port_DataReceived;
            _channel = channel;

            if (_channel == null) _channel = new ByteArrayChannel();

            
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

        public void Write(object data)
        {
            var bytes = _channel.ConvertFrom(data);
            _port.Write(bytes, 0, bytes.Length);
        }

        public void Dispose()
        {
            if (_port != null)
            {
                if (_port.IsOpen) _port.Close();
                _port.Dispose();
            }
        }
        void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            GetBytes(_port);
        }


        /// <summary>
        /// Get a byte[] from the serial port.
        /// Will keep reading until the port returns a 0.  0 bytes were read on the port
        /// </summary>
        /// <returns>The byte[] that has been sent by the other application</returns>
        protected byte[] GetBytes(SerialPort port)
        {
            byte[] bigBuffer = null;
            while (true)
            {
                //new up a buffer to read the available bytes
                byte[] buffer = new byte[port.BytesToRead];
                //read all of the available bytes, only read the buffer length
                int count = port.Read(buffer, 0, buffer.Length);
                //combine what we had before with the new data
                bigBuffer = CombineArrays(buffer, bigBuffer);
                if (count <= 0)
                {
                    var result = _channel.ConvertTo(bigBuffer);
                    if (OnReceived != null) OnReceived(result, _port, _channel, DateTime.Now);
                }  

            }

            return bigBuffer;
        }


        /// <summary>
        /// Append source to the end of destination
        /// Resize or new up destination if needed.
        /// We assume that destination is full, so no need to keep track of the last byte used, 
        /// just extend and copy source into
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private static byte[] CombineArrays(byte[] source, byte[] destination)
        {
            int destPointer = 0;
            //if we are starting with nothing in our destination, lets new it up with the same size as our source length
            if (destination == null)
            {
                destination = new byte[source.Length];
            }
            else
            {
                //destination already has data
                destPointer = destination.Length;
                //lets create a new buffer, that can handle both sets of data
                var newDest = new byte[source.Length + destination.Length];
                //copy destination into the first bytes of the newly allocated array
                destination.CopyTo(newDest, 0);
                //set destination to our larger buffer
                destination = newDest;
            }

            //push the source bits into the trailing slots of our final destination
            source.CopyTo(destination, destPointer);
            return destination;
        }
 
    }
}