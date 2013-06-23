using System;
using System.Globalization;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Agent.Contrib.Hardware;
using Agent.Contrib.Notifications;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace AnalogWatchFace
{
    public class AnalogFace : IFace
    {
        private IProvideNotifications _notificationProvider;

        public AnalogFace(IProvideNotifications notificationProvider)
        {
            _notificationProvider = notificationProvider;
            notificationProvider.OnNotificationReceived += notificationProvider_OnNotificationReceived;
        }

        private void notificationProvider_OnNotificationReceived(INotification notification)
        {
            if (_screen != null)
            {
                _screen.Clear();
                Render(_screen);
                _screen.Flush();
            }
        }

        public Agent.Contrib.Settings.ISettings Settings { get; set; }
        private Font font = Resources.GetFont(Resources.FontResources.small);

        private Bitmap img = new Bitmap(Resources.GetBytes(Resources.BinaryResources.WatchFaceFromScratch),
                                        Bitmap.BitmapImageType.Gif);


        private Drawing drawing = new Drawing();
        private Bitmap _screen = null;

        public void Render(Bitmap screen)
        {
            if (_screen == null) _screen = screen;
            screen.DrawImage(0, 0, img, 0, 0, img.Width, img.Height);


            var text = "AGENT";
            Point textLocation = new Point(
                AGENT.Center.X - (drawing.MeasureString(text, font)/2), AGENT.Center.Y - 25);
            screen.DrawText(text, font, Color.White, textLocation.X, textLocation.Y);

            var date = Settings.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.MonthDayPattern);
            ; //time.MonthNameShort + " " + time.Day;

            Point dateLocation = new Point(
                AGENT.Center.X - (drawing.MeasureString(date, font)/2), AGENT.Center.Y + 20);
            screen.DrawText(date, font, Color.White, dateLocation.X, dateLocation.Y);

            //draw our hands
            drawing.PaintSkinnyHands(screen, Settings.Now, AGENT.Center);

            //battery level
            drawing.DrawBattery(screen, new Point(1, 0), 14, 9, 1, Color.White, Color.Black, Battery.Charging,
                                Battery.Level);
            screen.DrawText(Battery.Level.ToString(), font, Color.White, 15, -2);

            drawing.DrawTray(screen, _notificationProvider, font);


        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
