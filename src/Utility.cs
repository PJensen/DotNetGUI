using DotNetGUI.Interfaces;
using System;

namespace DotNetGUI
{
    /// <summary>
    /// Utility
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Set
        /// </summary>
        /// <param name="g"> </param>
        public static void Set(this IColorScheme g)
        {
            Console.ForegroundColor = g.ForegroundColor;
            Console.BackgroundColor = g.BackgroundColor;
        }

        /// <summary>
        /// CursorState,
        ///     Basic cursor information.
        /// </summary>
        public class CursorState : IColorScheme, IEquatable<CursorState>
        {
            /// <summary>
            /// Create a new CursorState object
            /// </summary>
            /// <param name="x">x-coordinate</param>
            /// <param name="y">y-coordindate</param>
            /// <param name="fg">foreground colour</param>
            /// <param name="bg">background colour</param>
            public CursorState(int x, int y, ConsoleColor fg, ConsoleColor bg)
            {
                X = x;
                Y = y;

                ForegroundColor = fg;
                BackgroundColor = bg;
            }

            /// <summary>
            /// the x-coordinate
            /// </summary>
            public int X { get; set; }

            /// <summary>
            /// the y-coordinate
            /// </summary>
            public int Y { get; set; }

            /// <summary>
            /// the background colour
            /// </summary>
            public ConsoleColor BackgroundColor { get; set; }

            /// <summary>
            /// The foreground colour
            /// </summary>
            public ConsoleColor ForegroundColor { get; set; }

            /// <summary>
            /// Equals
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public bool Equals(CursorState other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return ForegroundColor == other.ForegroundColor &&
                    BackgroundColor == other.BackgroundColor &&
                    Y == other.Y &&
                    X == other.X;
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
                if (obj.GetType() != GetType()) return false;
                return Equals((CursorState)obj);
            }

            /// <summary>
            /// GetHashCode
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (int)ForegroundColor;
                    hashCode = (hashCode * 397) ^ (int)BackgroundColor;
                    hashCode = (hashCode * 397) ^ Y;
                    hashCode = (hashCode * 397) ^ X;
                    return hashCode;
                }
            }
        }
    }
}
