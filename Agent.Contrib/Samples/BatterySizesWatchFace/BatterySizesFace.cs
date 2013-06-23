using System;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace BatterySizesWatchFace
{
    public class BatterySizesFace : IFace
    {
        public int width = AGENT.Size/2;
        public bool charging = true;
        private Drawing drawing = new Drawing();
        private Font font = Resources.GetFont(Resources.FontResources.NinaB);
        public void Render(Microsoft.SPOT.Bitmap screen)
        {
           
            int height = width/2;
            
            drawing.DrawBattery(screen, new Point(AGENT.Size/2 - width/2, AGENT.Size/2 - height/2), width, height, 1,
                                Color.White, Color.Black, charging, Agent.Contrib.Hardware.Battery.Level);

            screen.DrawText("width:" + width.ToString() + ", height:" + height.ToString(), font, Color.White, 0,
                            AGENT.Size - font.Height);
         
        }

        public int UpdateSpeed
        {
            get { return int.MaxValue; }
        }
    }
}