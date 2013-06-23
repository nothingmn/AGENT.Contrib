using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Drawing
{
    public class Point
    {

        public Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }


    }

}