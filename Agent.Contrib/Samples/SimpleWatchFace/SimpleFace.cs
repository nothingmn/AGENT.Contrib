using System.Globalization;
using Agent.Contrib.Face;
using Agent.Contrib.Settings;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace SimpleWatchFace
{
    public class SimpleFace : IFace
    {
        public ISettings Settings { get; set; }
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
        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
