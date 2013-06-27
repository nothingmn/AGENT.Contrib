using System;
using System.Globalization;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Agent.Contrib.Hardware;
using Agent.Contrib.Notifications;
using Agent.Contrib.Settings;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace AnalogWatchFace
{
    public class AnalogFace : FaceWithTrayBase, IFace
    {

        public AnalogFace(IProvideNotifications notificationProvider, ISettings settings)
            : base(notificationProvider, settings)
        {

        }

        private Bitmap img = new Bitmap(Resources.GetBytes(Resources.BinaryResources.WatchFaceFromScratch),
                                        Bitmap.BitmapImageType.Gif);
        //private Bitmap img = new Bitmap(Resources.GetBytes(Resources.BinaryResources.AnalogTicksOutside),
        //                                        Bitmap.BitmapImageType.Gif);
        //private Bitmap img = new Bitmap(Resources.GetBytes(Resources.BinaryResources.AnalogTicksInside),
        //                                        Bitmap.BitmapImageType.Gif);


        public override void Render(Bitmap screen)
        {
            if (_screen == null) _screen = screen;

            screen.DrawImage(0, 0, img, 0, 0, img.Width, img.Height);

            var text = "AGENT";
            Point textLocation = new Point(
                AGENT.Center.X - (drawing.MeasureString(text, smallFont) / 2), AGENT.Center.Y - 25);
            screen.DrawText(text, smallFont, Color.White, textLocation.X, textLocation.Y);

            var date = Settings.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.MonthDayPattern);
            ; //time.MonthNameShort + " " + time.Day;

            Point dateLocation = new Point(
                AGENT.Center.X - (drawing.MeasureString(date, smallFont) / 2), AGENT.Center.Y + 20);
            screen.DrawText(date, smallFont, Color.White, dateLocation.X, dateLocation.Y);

            //draw our hands
            drawing.PaintSkinnyHands(screen, Settings.Now, AGENT.Center);


            
            drawing.DrawTray(screen, _notificationProvider, smallFont);

        }

        public new int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
