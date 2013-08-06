using System;
using System.Globalization;
using AGENT.Contrib;
using AGENT.Contrib.Drawing;
using AGENT.Contrib.Face;
using AGENT.Contrib.Settings;
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
        public ISettings Settings { get; set; }
        public bool ShowDate { get; set; }
        private Font font = Resources.GetFont(Resources.FontResources.NinaB);

        public void Render(Bitmap screen)
        {

            screen.DrawText(
                Settings.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortTimePattern),
                font,
                Color.White,
                0,
                0
                );

            if (ShowDate)
            {
                screen.DrawText(
                    Settings.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern),
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