using System;
using Agent.Contrib.Face;
using Agent.Contrib.Hardware;
using Microsoft.SPOT;

namespace SimpleWatchFace
{
    public class Program
    {

        public static void Main()
        {

            var watch = new WatchFace(new SimpleFace());
            watch.Start();
        }

    }
}
