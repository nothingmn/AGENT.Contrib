using System;
using System.Globalization;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace AnalogWatchFace
{
    public class AnalogFace : IFace
    {
        public Agent.Contrib.Settings.ISettings Settings { get; set; }
        private Font font = Resources.GetFont(Resources.FontResources.small);

        private Bitmap img = new Bitmap(Resources.GetBytes(Resources.BinaryResources.WatchFaceFromScratch),
                                Bitmap.BitmapImageType.Gif);

        private Drawing drawing = new Drawing();
        public void Render(Bitmap screen)
        {

            screen.DrawImage(0, 0, img, 0, 0, img.Width, img.Height);


            var text = "AGENT";
            Point textLocation = new Point(
                AGENT.Center.X - (drawing.MeasureString(text, font) / 2), AGENT.Center.Y - 25);
            screen.DrawText(text, font, Color.White, textLocation.X, textLocation.Y);

            var date = Settings.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.MonthDayPattern); ;//time.MonthNameShort + " " + time.Day;

            Point dateLocation = new Point(
                AGENT.Center.X - (drawing.MeasureString(date, font) / 2), AGENT.Center.Y + 20);
            screen.DrawText(date, font, Color.White, dateLocation.X, dateLocation.Y);

            
            drawing.PaintSkinnyHands(screen, Settings.Now, AGENT.Center);

        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
