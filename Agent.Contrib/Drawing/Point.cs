using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Drawing
{
    public struct Point
    {
        public Point(int x = 0, int y = 0)
        {
            if (x > short.MaxValue)
                throw new ArgumentOutOfRangeException("x");

            if (y > short.MaxValue)
                throw new ArgumentOutOfRangeException("y");

            X = (short)x;
            Y = (short)y;
        }

        public Point(short x = 0, short y = 0)
        {
            X = x;
            Y = y;
        }

        public short X;
        public short Y;


    }

}