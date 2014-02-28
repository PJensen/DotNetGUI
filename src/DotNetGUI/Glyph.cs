using System;

namespace DotNetGUI
{
    /// <summary>
    /// Glyph
    /// <remarks>A glyph is a combination of a character; a foreground color and a background color.</remarks>
    /// </summary>
    public struct Glyph : IEquatable<Glyph>
    {
        /// <summary>
        /// The character for this glyph
        /// </summary>
        public char G { get; set; }

        /// <summary>
        /// Foreground ConsoleColor
        /// </summary>
        public ConsoleColor FG { get; set; }

        /// <summary>
        /// Background ConsoleColor
        /// </summary>
        public ConsoleColor BG { get; set; }

        /// <summary>
        /// Glyph
        /// </summary>
        /// <param name="g">char</param>
        /// <param name="fg">fg</param>
        /// <param name="bg">bg</param>
        public Glyph(char g, ConsoleColor fg, ConsoleColor bg)
            : this()
        {
            G = g;
            FG = fg;
            BG = bg;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Glyph other)
        {
            return Equals(other.BG, BG) && Equals(other.FG, FG) && other.G == G;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Glyph && Equals((Glyph)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                var result = BG.GetHashCode();
                result = (result * 397) ^ FG.GetHashCode();
                result = (result * 397) ^ G.GetHashCode();
                return result;
            }
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="left">left hand side</param>
        /// <param name="right">right hand side</param>
        /// <returns></returns>
        public static bool operator ==(Glyph left, Glyph right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equals
        /// </summary>
        /// <param name="left">left hand side</param>
        /// <param name="right">right hand side</param>
        /// <returns>true if not equal</returns>
        public static bool operator !=(Glyph left, Glyph right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>a string representation of this glyph</returns>
        public override string ToString()
        {
            return string.Format("G: {0}, BG: {1}, FG: {2}", G, BG, FG);
        }
    }
}