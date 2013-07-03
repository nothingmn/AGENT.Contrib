using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Drawing
{
    public class Size
    {
        public Size()
        {
        }

        public Size(int width, int height)
            : this()
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
