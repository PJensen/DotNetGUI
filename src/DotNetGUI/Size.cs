using System;

namespace DotNetGUI
{
    /// <summary>
    /// The <see cref="Size"/>  structure represents a <see cref="Width"/> and <see cref="Height"/>
    /// <remarks>A <see cref="Size"/> cannot have a <c>negative</c> width or height</remarks>
    /// </summary>
    public struct Size : IEquatable<Size>
    {
        #region backing store

        private readonly int _width;
        private readonly int _height;

        #endregion

        /// <summary>
        /// Creates a new size structure with the passed <see cref="Width"/> and <see cref="Height"/>.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Size(int width, int height)
        {
            if (width < 0 && height < 0)
                throw new DotNetGUIException("width and height cannot be negative");

            if (width < 0)
                throw new DotNetGUIException("width cannot be negative");

            if (height < 0)
                throw new DotNetGUIException("height cannot be negative");

            _width = width;
            _height = height;
        }

        /// <summary>
        /// The <see cref="Width"/> of this <see cref="Size"/> structure
        /// </summary>
        public int Width { get { return _width; } }

        /// <summary>
        /// The <see cref="Height"/> of this <see cref="Size"/> structure
        /// </summary>
        public int Height { get { return _height; } }

        /// <summary>
        /// ToString implementation for <see cref="Size"/>
        /// </summary>
        /// <returns>a string representation of this <see cref="Size"/> object</returns>
        public override string ToString()
        {
            return string.Format("Width: {0}, Height: {1}", Width, Height);
        }

        /// <summary>
        /// The equals comparison for this <see cref="Size"/> object
        /// </summary>
        /// <param name="other">the other size object</param>
        /// <returns><value>true</value> if equal</returns>
        public bool Equals(Size other)
        {
            return _height == other._height && _width == other._width;
        }

        /// <summary>
        /// Equals for this size object
        /// </summary>
        /// <param name="obj">the object to compare to</param>
        /// <returns>true if equal</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Size && Equals((Size)obj);
        }

        /// <summary>
        /// GetHashCode implementation for <see cref="Size"/>
        /// </summary>
        /// <returns>the hashcode for this object</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (_height * 397) ^ _width;
            }
        }

        /// <summary>
        /// Equals operator for <see cref="Size"/>
        /// </summary>
        /// <param name="left">the left hand side</param>
        /// <param name="right">the right hand side</param>
        /// <returns></returns>
        public static bool operator ==(Size left, Size right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equals operator for <see cref="Size"/>
        /// </summary>
        /// <param name="left">the left hand side</param>
        /// <param name="right">the right hand side</param>
        /// <returns>true if the two are not equal</returns>
        public static bool operator !=(Size left, Size right)
        {
            return !left.Equals(right);
        }
    }
}
