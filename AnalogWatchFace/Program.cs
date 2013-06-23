using System;
using Agent.Contrib.Face;
using Microsoft.SPOT;

namespace AnalogWatchFace
{
    public class Program
    {
        public static void Main()
        {
            new WatchFace(new AnalogFace()).Start();
        }

    }
}
