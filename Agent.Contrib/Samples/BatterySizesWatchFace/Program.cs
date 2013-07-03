using System;
using Agent.Contrib;
using Agent.Contrib.Face;
using Agent.Contrib.Hardware;
using Microsoft.SPOT;

namespace BatterySizesWatchFace
{
    public class Program
    {
        private static IFace face = new BatterySizesFace();
        private static WatchFace watch;

        public static void Main()
        {
            ButtonHelper.ButtonSetup = new Buttons[]{ Buttons.TopRight, Buttons.BottomRight, Buttons.MiddleRight, Buttons.TopLeft };
            ButtonHelper.Current.OnButtonPress += Current_OnButtonPress;
            watch = new WatchFace(face);
            watch.Start();
        }

        static void Current_OnButtonPress(Buttons button, Microsoft.SPOT.Hardware.InterruptPort port, ButtonDirection direction, DateTime time)
        {
            if (direction == ButtonDirection.Up)
            {
                if (button == Buttons.TopRight)
                {
                    (face as BatterySizesFace).width++;
                }
                else if (button == Buttons.BottomRight)
                {
                    (face as BatterySizesFace).width--;
                }
                else if (button == Buttons.MiddleRight)
                {
                    (face as BatterySizesFace).width = AGENT.Size/2;
                }
                else
                {
                    (face as BatterySizesFace).charging = !(face as BatterySizesFace).charging;
                }
                watch.Render();
            }
        }


    }
}
