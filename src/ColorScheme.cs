using DotNetGUI.Interfaces;
using System;

namespace DotNetGUI
{
    /// <summary>
    /// ColorScheme
    /// </summary>
    public class ColorScheme : IColorScheme, IEquatable<ColorScheme>
    {
        /// <summary>
        /// ColorScheme
        /// </summary>
        /// <param name="fg">the foreground color</param>
        /// <param name="bg">the background color</param>
        public ColorScheme(ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            BackgroundColor = bg;
            ForegroundColor = fg;
        }

        /// <summary>
        /// BackgroundColor
        /// </summary>
        public ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// ForegroundColor
        /// </summary>
        public ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        /// Returns a new color scheme
        /// </summary>
        /// <param name="fg"></param>
        /// <param name="bg"></param>
        /// <returns></returns>
        internal static ColorScheme New(ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            return new ColorScheme(fg, bg);
        }

        /// <summary>
        /// ColorScheme
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ColorScheme other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ForegroundColor == other.ForegroundColor && BackgroundColor == other.BackgroundColor;
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
            return obj.GetType() == GetType() && Equals((ColorScheme) obj);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) ForegroundColor*397) ^ (int) BackgroundColor;
            }
        }
    }
}
