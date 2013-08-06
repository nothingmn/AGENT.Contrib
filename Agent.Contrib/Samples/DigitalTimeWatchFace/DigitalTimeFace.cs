using System;
using AGENT.Contrib;
using AGENT.Contrib.Drawing;
using AGENT.Contrib.Face;
using AGENT.Contrib.Notifications;
using AGENT.Contrib.Settings;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace DigitalTimeWatchFace
{
    public class DigitalTimeFace : FaceWithTrayBase, IFace
    {
        
        public DigitalTimeFace(IProvideNotifications notificationProvider, ISettings settings) : base(notificationProvider, settings)
        {

        }

        private Font font = Resources.GetFont(Resources.FontResources.Digital714Full);
        private Font bigfont = Resources.GetFont(Resources.FontResources.Digital748TimeOnly);

        public override void Render(Bitmap screen)
        {
            if (base._screen == null) _screen = screen;

 
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
            screen.DrawLine(Color.White, 2, 0, AGENT.Contrib.Device.Size/2, AGENT.Contrib.Device.Size, AGENT.Contrib.Device.Size/2);

            int left = AGENT.Contrib.Device.Size - base.drawing.MeasureString(display, bigfont);
            screen.DrawText(display, bigfont, Color.White, left, (AGENT.Contrib.Device.Size/2) + 2);

            string dow = System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames[(int) now.DayOfWeek];
            screen.DrawText(dow.ToString(), font, Color.White, 5, 15);

            string date = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames[(int) now.Month];
            date = date + " " + now.Day.ToString();
            screen.DrawText(date, font, Color.White, 5, 35);

            drawing.DrawTray(screen, _notificationProvider, smallFont);


        }

    }
}