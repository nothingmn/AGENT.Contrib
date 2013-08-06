using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;

namespace $safeprojectname$
{
    public class Program
    {
        static Bitmap _display;
        static Timer _updateClockTimer;

        public static void Main()
        {
            // initialize our display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            // display the time immediately
            UpdateTime(null);

            // obtain the current time
            DateTime currentTime = DateTime.Now;
            // set up timer to refresh time every minute
            TimeSpan dueTime = new TimeSpan(0, 0, 0, 59 - currentTime.Second, 1000 - currentTime.Millisecond); // start timer at beginning of next minute
            TimeSpan period = new TimeSpan(0, 0, 1, 0, 0); // update time every minute
            _updateClockTimer = new Timer(UpdateTime, null, dueTime, period); // start our update timer

            // go to sleep; time updates will happen automatically every minute
            Thread.Sleep(Timeout.Infinite);
        }

        static void UpdateTime(object state)
        {
            // obtain the current time
            DateTime currentTime = DateTime.Now;
            // clear our display buffer
            _display.Clear();

            // add your watchface drawing code here
            Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);
            _display.DrawText(currentTime.Hour.ToString("D2") + ":" + currentTime.Minute.ToString("D2"), fontNinaB, Color.White, 46, 58);

            // flush the display buffer to the display
            _display.Flush();
        }

    }
}
