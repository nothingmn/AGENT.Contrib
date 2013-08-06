using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.Drawing
{
    /// <summary>
    /// Stores an ordered pair of integers, which specify Height and Width
    /// </summary>
    public struct Size : ICloneable
    {
        private int m_Width;
        private int m_Height;

        /// <summary>
        /// Gets or sets the horizontal component of this Size structure
        /// </summary>
        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        /// <summary>
        /// Gets or sets the vertical component of this Size structure
        /// </summary>
        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        /// <summary>
        /// Initializes a new instance of the System.Drawing.Size structure from the specified dimensions.
        /// </summary>
        /// <param name="width">the width component of the new Size.</param>
        /// <param name="height">the height component of the new Size.</param>
        public Size(int width = 0, int height = 0)
        {
            m_Width = width;
            m_Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the System.Drawing.Size structure from the specified System.Drawing.Point structure.
        /// </summary>
        /// <param name="pt">The System.Drawing.Point structure from which to initialize this Size structure.</param>
        public Size(Point pt)
            : this(pt.X, pt.Y)
        {
        }

        /// <summary>
        /// Tests to see whether the specified object is a Size structure with the same dimensions as this Size structure.
        /// </summary>
        /// <param name="obj">The Object to test</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Size && Equals((Size)obj);
        }

        /// <summary>
        /// Specifies whether this System.Drawing.Size structure has the same dimensions as the specified System.Drawing.Size structure.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Size other)
        {
            return m_Width == other.m_Width && m_Height == other.m_Height;
        }

        public override int GetHashCode()
        {
            // auto-generated via resharper
            unchecked
            {
                return (m_Width * 397) ^ m_Height;
            }
        }

        /// <summary>
        /// Tests whether this System.Drawing.Size structure has width and height of 0.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty
        {
            get { return m_Width == 0 && m_Height == 0; }
        }

        public override string ToString()
        {
            return "{Width=" + Width + ", Height=" + Height + "}";
        }

        // here is where it differs from the actual System.Drawing.Size

        public Object Clone()
        {
            return new Size(Width, Height);
        }

        public Point CenterWithin(Rectangle container)
        {
            return new Point(((container.Width - Width) >> 1) + container.X, ((container.Height - Height) >> 1) + container.Y);
        }
    }
}
