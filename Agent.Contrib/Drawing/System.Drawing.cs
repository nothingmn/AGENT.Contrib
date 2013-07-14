/*
 * This file is meant to duplicate some of the functionality found in the full .NET System.Drawing namespace.
 * Currently, it contains implementations of Size, Point, and Rectangle.
 * 
 * Some of the Rectangle methods have not been implemented.
 */

// ReSharper disable CheckNamespace
namespace System.Drawing
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
		public Size (int width = 0, int height = 0)
		{
			m_Width = width;
			m_Height = height;
		}
		
		/// <summary>
		/// Initializes a new instance of the System.Drawing.Size structure from the specified System.Drawing.Point structure.
		/// </summary>
		/// <param name="pt">The System.Drawing.Point structure from which to initialize this Size structure.</param>
		public Size (Point pt) : this (pt.X, pt.Y)
		{
		}

		/// <summary>
		/// Tests to see whether the specified object is a Size structure with the same dimensions as this Size structure.
		/// </summary>
		/// <param name="obj">The Object to test</param>
		/// <returns></returns>
		public override bool Equals (object obj)
		{
			if (ReferenceEquals (null, obj)) return false;
			return obj is Size && Equals ((Size) obj);
		}

		/// <summary>
		/// Specifies whether this System.Drawing.Size structure has the same dimensions as the specified System.Drawing.Size structure.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals (Size other)
		{
			return m_Width == other.m_Width && m_Height == other.m_Height;
		}

		public override int GetHashCode ()
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

		public override string ToString ()
		{
			return "{Width=" + Width + ", Height=" + Height + "}";
		}

		// here is where it differs from the actual System.Drawing.Size

		public Object Clone ()
		{
			return new Size (Width, Height);
		}

		public Point CenterWithin (Rectangle container)
		{
			return new Point (((container.Width - Width) >> 1) + container.X, ((container.Height - Height) >> 1) + container.Y);
		}
	}

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
		public Point (int x = 0 , int y = 0)
		{
			m_X = x;
			m_Y = y;
		}

		/// <summary>
		/// Initializes a new instance of the System.Drawing.Point class from a System.Drawing.Size.
		/// </summary>
		/// <param name="sz">A System.Drawing.Size that specifies the coordinates for the new System.Drawing.Point.</param>
		public Point (Size sz) : this (sz.Width, sz.Height)
		{
		}

		/// <summary>
		/// Specifies whether this System.Drawing.Point contains the same coordinates as the specified System.Object
		/// </summary>
		/// <param name="obj">The System.Object to test.</param>
		/// <returns></returns>
		public override bool Equals (object obj)
		{
			if (ReferenceEquals (null, obj)) return false;
			return obj is Point && Equals ((Point) obj);
		}

		/// <summary>
		/// Specifies whether this System.Drawing.Point contains the same coordinates as the specified System.Drawing.Point
		/// </summary>
		/// <param name="other">The other System.Drawing.Point to test.</param>
		/// <returns></returns>
		public bool Equals (Point other)
		{
			return m_X == other.m_X && m_Y == other.m_Y;
		}

		public override int GetHashCode ()
		{
			// auto-generated via resharper
			unchecked
			{
				return (m_X*397) ^ m_Y;
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
		public void Offset (Point p)
		{
			Offset (p.X, p.Y);
		}

		/// <summary>
		/// Translates this Point by the specified amount
		/// </summary>
		/// <param name="dx"></param>
		/// <param name="dy"></param>
		public void Offset (int dx, int dy)
		{
			X += dx;
			Y += dy;
		}

		public override string ToString ()
		{
			return "{X=" + X + ",Y=" + Y + "}";
		}

		// here is where it differs from the actual System.Drawing.Point

		public Object Clone ()
		{
			return new Point (X, Y);
		}
	}

	/// <summary>
	/// Stores a set of four integers that represent the location and size of a rectangle
	/// </summary>
	public struct Rectangle
	{
		private int m_X;
		private int m_Y;
		private int m_Width;
		private int m_Height;

		/// <summary>
		/// Gets or sets the x-coordinate of the upper-left corner of this System.Drawing.Rectangle structure.
		/// </summary>
		public int X
		{
			get { return m_X; }
			set { m_X = value; }
		}

		/// <summary>
		/// Gets the x-coordinate of the left edge of this System.Drawing.Rectangle structure.
		/// </summary>
		public int Left
		{
			get { return m_X; }
		}

		/// <summary>
		/// Gets or sets the y-coordinate of the upper-left corner of this System.Drawing.Rectangle structure.
		/// </summary>
		public int Y
		{
			get { return m_Y; }
			set { m_Y = value; }
		}

		/// <summary>
		/// Gets the y-coordinate of the top edge of this System.Drawing.Rectangle structure.
		/// </summary>
		public int Top
		{
			get { return m_Y; }
		}

		/// <summary>
		/// Gets or sets the width of this System.Drawing.Rectangle structure.
		/// </summary>
		public int Width
		{
			get { return m_Width; }
			set { m_Width = value; }
		}

		/// <summary>
		/// Gets the x-coordinate that is the sum of the X and Width property values of this System.Drawing.Rectangle structure.
		/// </summary>
		public int Right
		{
			get { return m_X + m_Width; }
		}

		/// <summary>
		/// Gets or sets the height of this System.Drawing.Rectangle structure.
		/// </summary>
		public int Height
		{
			get { return m_Height; }
			set { m_Height = value; }
		}

		/// <summary>
		/// Gets the y-coordinate that is the sum of the Y and Height property values of this System.Drawing.Rectangle structure.
		/// </summary>
		public int Bottom
		{
			get { return m_Y + m_Height; }
		}

		/// <summary>
		/// Gets or sets the coordinates of the upper-left corner of this System.Drawing.Rectangle.
		/// </summary>
		public Point Location
		{
			get { return new Point (m_X, m_Y); }
			set
			{
				m_X = value.X;
				m_Y = value.Y;
			}
		}

		/// <summary>
		/// Gets or sets the size of this System.Drawing.Rectangle.
		/// </summary>
		public Size Size
		{
			get { return new Size (m_Width, m_Height); }
			set
			{
				m_Width = value.Width;
				m_Height = value.Height;
			}
		}

		/// <summary>
		/// Tests whether all numeric properties of this System.Drawing.Rectangle have values of zero.
		/// </summary>
		public bool IsEmpty
		{
			get { return m_X == 0 && m_Y == 0 && m_Width == 0 && m_Height == 0; }
		}

		/// <summary>
		/// Initializes a new instance of the System.Drawing.Rectangle class with the specified location and size.
		/// </summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle.</param>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		public Rectangle (int x = 0, int y = 0, int width = 0, int height = 0)
		{
			m_X = x;
			m_Y = y;
			m_Width = width;
			m_Height = height;
		}

		/// <summary>
		/// Initializes a new instance of the System.Drawing.Rectangle class with the specified location and size.
		/// </summary>
		/// <param name="location">A System.Drawing.Point that represents the upper-left corner of the rectangular region.</param>
		/// <param name="size">A System.Drawing.Size that represents the width and height of the rectangular region.</param>
		public Rectangle (Point location, Size size) : this (location.X, location.Y, size.Width, size.Height)
		{
		}

		/// <summary>
		/// Tests whether obj is a System.Drawing.Rectangle structure with the same location and size of this System.Drawing.Rectangle structure.
		/// </summary>
		/// <param name="obj">The System.Object to test</param>
		/// <returns></returns>
		public override bool Equals (object obj)
		{
			if (ReferenceEquals (null, obj)) return false;
			return obj is Rectangle && Equals ((Rectangle) obj);
		}

		/// <summary>
		/// Tests whether the System.Drawing.Rectangle structure has the same location and size of this System.Drawing.Rectangle structure.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals (Rectangle other)
		{
			return m_X == other.m_X && m_Y == other.m_Y && m_Width == other.m_Width && m_Height == other.m_Height;
		}

		public override int GetHashCode ()
		{
			unchecked
			{
				var hashCode = m_X;
				hashCode = (hashCode * 397) ^ m_Y;
				hashCode = (hashCode * 397) ^ m_Width;
				hashCode = (hashCode * 397) ^ m_Height;
				return hashCode;
			}
		}

		/// <summary>
		/// Adjusts the location of this rectangle by the specified amount.
		/// </summary>
		/// <param name="x">The horizontal offset.</param>
		/// <param name="y">The vertical offset.</param>
		public void Offset (int x, int y)
		{
			m_X += x;
			m_Y += y;
		}

		/// <summary>
		/// Adjusts the location of this rectangle by the specified amount.
		/// </summary>
		/// <param name="pos">Amount to offset the location.</param>
		public void Offset (Point pos)
		{
			Offset (pos.X, pos.Y);
		}

/*
		/// <summary>
		/// Determines if the specified point is contained within this System.Drawing.Rectangle structure.
		/// </summary>
		/// <param name="x">The x-coordinate of the point to test.</param>
		/// <param name="y">The y-coordinate of the point to test.</param>
		public bool Contains (int x, int y)
		{
			// TODO implement
			return false;
		}

		/// <summary>
		/// Determines if the specified point is contained within this System.Drawing.Rectangle structure.
		/// </summary>
		/// <param name="pt">The System.Drawing.Point to test.</param>
		public bool Contains (Point pt)
		{
			// TODO implement
			return false;
		}

		/// <summary>
		/// Determines if the rectangular region represented by rect is entirely contained within this System.Drawing.Rectangle structure.
		/// </summary>
		/// <param name="rect">The System.Drawing.Rectangle to test.</param>
		public bool Contains (Rectangle rect)
		{
			// TODO implement
			return false;
		}

		/// <summary>
		/// Replaces this System.Drawing.Rectangle with the intersection of itself and the specified System.Drawing.Rectangle.
		/// </summary>
		/// <param name="rect">The System.Drawing.Rectangle with which to intersect.</param>
		public void Intersect (Rectangle rect)
		{
			// TODO implement
		}

		/// <summary>
		/// Determines if this rectangle intersects with rect.
		/// </summary>
		/// <param name="rect">The rectangle to test.</param>
		public bool IntersectsWith (Rectangle rect)
		{
			// TODO implement
			return false;
		}
*/

		/// <summary>
		/// Enlarges this System.Drawing.Rectangle by the specified amount.
		/// </summary>
		/// <param name="width">The amount to inflate this System.Drawing.Rectangle horizontally.</param>
		/// <param name="height">The amount to inflate this System.Drawing.Rectangle vertically.</param>
		public void Inflate (int width, int height)
		{
			m_Width += width;
			m_Height += height;
		}

		/// <summary>
		/// Enlarges this System.Drawing.Rectangle by the specified amount.
		/// </summary>
		/// <param name="size">The amount to inflate this System.Drawing.Rectangle</param>
		public void Inflate (Size size)
		{
			m_Width += size.Width;
			m_Height += size.Height;
		}

		public override string ToString ()
		{
			return "{X=" + X + ",Y=" + Y + ",Width=" + Width + ",Height=" + Height + "}";
		}

		// here is where it differs from the actual System.Drawing.Rectangle

		public Object Clone ()
		{
			return new Rectangle (X, Y, Width, Height);
		}

		public Point CenterWithin (Rectangle container)
		{
			return new Point (((container.Width - Width) >> 1) + container.X, ((container.Height - Height) >> 1) + container.Y);
		}
	}
}