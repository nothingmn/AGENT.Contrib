using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.Drawing
{
    /// <summary>
    /// Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.
    /// </summary>
    public struct Point : ICloneable
    {
        private int m_X;
        private int m_Y;

        /// <summary>
        /// Gets or sets the x-coordinate of this Point
        /// </summary>
        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of this Point
        /// </summary>
        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        /// <summary>
        /// Initializes a new instance of the System.Drawing.Point class with the specified coordinates.
        /// </summary>
        /// <param name="x">The horizontal position of the point.</param>
        /// <param name="y">The vertical position of the point.</param>
        public Point(int x = 0, int y = 0)
        {
            m_X = x;
            m_Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the System.Drawing.Point class from a System.Drawing.Size.
        /// </summary>
        /// <param name="sz">A System.Drawing.Size that specifies the coordinates for the new System.Drawing.Point.</param>
        public Point(Size sz)
            : this(sz.Width, sz.Height)
        {
        }

        /// <summary>
        /// Specifies whether this System.Drawing.Point contains the same coordinates as the specified System.Object
        /// </summary>
        /// <param name="obj">The System.Object to test.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point)obj);
        }

        /// <summary>
        /// Specifies whether this System.Drawing.Point contains the same coordinates as the specified System.Drawing.Point
        /// </summary>
        /// <param name="other">The other System.Drawing.Point to test.</param>
        /// <returns></returns>
        public bool Equals(Point other)
        {
            return m_X == other.m_X && m_Y == other.m_Y;
        }

        public override int GetHashCode()
        {
            // auto-generated via resharper
            unchecked
            {
                return (m_X * 397) ^ m_Y;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this Point is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty
        {
            get { return m_X == 0 && m_Y == 0; }
        }

        /// <summary>
        /// Translates this System.Drawing.Point by the specified System.Drawing.Point
        /// </summary>
        /// <param name="p"></param>
        public void Offset(Point p)
        {
            Offset(p.X, p.Y);
        }

        /// <summary>
        /// Translates this Point by the specified amount
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Offset(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public override string ToString()
        {
            return "{X=" + X + ",Y=" + Y + "}";
        }

        // here is where it differs from the actual System.Drawing.Point

        public Object Clone()
        {
            return new Point(X, Y);
        }
    }
}