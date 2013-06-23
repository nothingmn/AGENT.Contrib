using System;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace DigitalTimeWatchFace
{
    public class DigitalTimeFace : IFace
    {
        public Agent.Contrib.Settings.ISettings Settings { get; set; }
        private Font font = Resources.GetFont(Resources.FontResources.Digital714Full);
        private Font bigfont = Resources.GetFont(Resources.FontResources.Digital748TimeOnly);
        private Drawing drawing = new Drawing();

        public void Render(Bitmap screen)
        {

            DateTime now = DateTime.Now;
            string display = "";
            string hour, minute = now.Minute.ToString();
            if (Settings.TimeInISONotatation)
            {
                hour = now.Hour.ToString();
            }
            else
            {
                int h = now.Hour;
                if (h >= 12) h = h - 12;
                if (h == 0) h = 12;
                hour = h.ToString();
            }
            if (minute.Length == 1) minute = "0" + minute;

            display = hour + ":" + minute;
            screen.DrawLine(Color.White, 2, 0, AGENT.Size / 2, AGENT.Size, AGENT.Size / 2);

            int left = AGENT.Size - drawing.MeasureString(display, bigfont);
            screen.DrawText(display, bigfont, Color.White, left, (AGENT.Size / 2) + 2);

            string dow = System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames[(int)now.DayOfWeek];
            screen.DrawText(dow.ToString(), font, Color.White, 5, 10);

            string date = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames[(int)now.Month];
            date = date + " " + now.Day.ToString();
            screen.DrawText(date, font, Color.White, 5, 30);
 
        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
