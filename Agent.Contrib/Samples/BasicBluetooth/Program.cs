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
    public class SimpleObject
    {
        public string AString { get; set; }
        public int AnInt { get; set; }
        public double ADouble { get; set; }
        public float AFlot { get; set; }
        public DateTime ADateTime { get; set; }
    }

    public class Program
    {
        private static Bitmap _display;


        private static IConnection _connection;

        public static void Main()
        {
            // initialize display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            // sample "hello world" code
            _display.Clear();
            Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);
            _display.DrawText("Waiting for data", fontNinaB, Color.White, 10, 64);
            _display.Flush();

            IChannel channel = new CSVChannel();
            _connection = new Connection("COM1", channel);
            _connection.OnReceived += _connection_OnReceived;
            _connection.Open();


            // go to sleep; all further code should be timer-driven or event-driven
            Thread.Sleep(Timeout.Infinite);
        }

        private static void _connection_OnReceived(object Data, System.IO.Ports.SerialPort port, IChannel channel, DateTime Timestamp)
        {
            //receive the data
            _display.Clear();
            Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);
            var display = (string)Data;
            try
            {
                var ary = (string[]) Data;
                if (ary != null)
                {
                    display = "";
                    for (int i = 0; i < ary.Length; i++)
                    {
                        display += ary[i] + ",";
                    }
                }
            }
            catch (Exception)
            {
            }
            _display.DrawText(display, fontNinaB, Color.White, 10, 64);
            _display.Flush();
            //echo it back
            _connection.Write(Data);
        }

    }
}