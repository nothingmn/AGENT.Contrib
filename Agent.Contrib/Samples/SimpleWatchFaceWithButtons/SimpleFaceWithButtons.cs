using System;
using System.Globalization;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace SimpleWatchFaceWithButtons
{
    public class SimpleFaceWithButtons : IFace
    {
        public SimpleFaceWithButtons()
        {
            ShowDate = false;
        }

        public bool ShowDate { get; set; }
        private Font font = Resources.GetFont(Resources.FontResources.NinaB);

        public void Render(Bitmap screen)
        {

            screen.DrawText(
                DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortTimePattern),
                font,
                Color.White,
                0,
                0
                );

            if (ShowDate)
            {
                screen.DrawText(
                    DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern),
                    font,
                    Color.White,
                    0,
                    font.Height + 5
                    );

            }
        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}