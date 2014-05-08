using System;

namespace DotNetGUI
{
    /// <summary>
    /// Point
    /// <remarks>A two dimensional point on the cartesian plane</remarks>
    /// </summary>
    public struct Point : IEquatable<Point>
    {
        /// <summary>
        /// The <see cref="Origin"/> is the point at (0,0)
        /// </summary>
        public static Point Origin = new Point();

        /// <summary>
        /// Creates a new <see cref="Point"/>
        /// </summary>
        /// <param name="x">the x-coordinate</param>
        /// <param name="y">the y-coordinate</param>
        public Point(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The x-coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The y-coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Returns a string representation of this point
        /// </summary>
        /// <returns>A string representation of this point</returns>
        public override string ToString()
        {
            return string.Format("Y: {0}, X: {1}", Y, X);
        }

        /// <summary>
        /// Returns true if this point equals the other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Point other)
        {
            return Y == other.Y && X == other.X;
        }

        /// <summary>
        /// Returns true of this object equals the other
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point) obj);
        }

        /// <summary>
        /// Returns a hashcode for this point
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Y*397) ^ X;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equals operator
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }
    }
}