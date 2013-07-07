using System;
using Agent.Contrib.Communication.Channels;
using Agent.Contrib.Hardware.Bluetooth;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;

namespace BasicBluetooth
{
    public class Program
    {
        static Bitmap _display;


        private static Connection _connection;
        public static void Main()
        {
            // initialize display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            // sample "hello world" code
            _display.Clear();
            Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);
            _display.DrawText("Waiting for data", fontNinaB, Color.White, 10, 64);
            _display.Flush();

            IChannel stringChannel = new StringChannel();
            _connection = new Connection("COM1", stringChannel);
            _connection.OnReceived += _connection_OnReceived;
            _connection.Open();


            // go to sleep; all further code should be timer-driven or event-driven
            Thread.Sleep(Timeout.Infinite);
        }

        static void _connection_OnReceived(object Data, System.IO.Ports.SerialPort port, IChannel channel, DateTime Timestamp)
        {
            //receive the data
            var received = (Data as string);
            _display.Clear();
            Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);
            _display.DrawText(received, fontNinaB, Color.White, 10, 64);
            _display.Flush();
            //echo it back
            _connection.Write(received);
        }

    }
}
