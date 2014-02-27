using DotNetGUI.Interfaces;
using System;
using System.Diagnostics;

namespace DotNetGUI
{
    /// <summary>
    /// Point
    /// </summary>
    [DebuggerDisplay("({X}, {Y})")]
    public class Point : IPoint, IEquatable<Point>
    {
        /// <summary>
        /// Point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X-Coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y-Coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Point other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Y == other.Y && X == other.X;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Point)obj);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Y * 397) ^ X;
            }
        }
    }
}
