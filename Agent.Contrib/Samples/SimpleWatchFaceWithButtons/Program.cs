using System;
using Agent.Contrib.Face;
using Agent.Contrib.Hardware;
using Microsoft.SPOT;

namespace SimpleWatchFaceWithButtons
{
    public class Program
    {
        private static IFace face = new SimpleFaceWithButtons();
        private static WatchFace watch;
        public static void Main()
        {

            ButtonHelper.Current.OnButtonPress += Current_OnButtonPress;
            watch = new WatchFace(face);
            watch.Start();
        }

        private static void Current_OnButtonPress(Buttons button, Microsoft.SPOT.Hardware.InterruptPort port,
                                                  ButtonDirection direction, DateTime time)
        {
            if (button == Buttons.MiddleRight && direction == ButtonDirection.Up)
            {
                (face as SimpleFaceWithButtons).ShowDate = !(face as SimpleFaceWithButtons).ShowDate;
                watch.Render();
            }
        }

    }
}