using System;
using Microsoft.SPOT;
using Agent.Contrib.Face;

namespace DigitalTimeWatchFace
{
    public class Program
    {
        public static void Main()
        {
            new WatchFace(new DigitalTimeFace()).Start();
        }

    }
}
