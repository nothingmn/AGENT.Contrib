using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;

namespace SimpleMemoryGame
{
    public class Program
    {
        static Bitmap _display;

        public static void Main()
        {
            // initialize display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            // sample "hello world" code
            _display.Clear();
            Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);
            _display.DrawText("Hello world.", fontNinaB, Color.White, 10, 64);
            _display.Flush();

            // go to sleep; all further code should be timer-driven or event-driven
            Thread.Sleep(Timeout.Infinite);
        }

    }
}
