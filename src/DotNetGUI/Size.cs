using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetGUI
{
    /// <summary>
    /// The <see cref="Size"/>  structure represents a <see cref="Width"/> and <see cref="Height"/>
    /// <remarks>A <see cref="Size"/> cannot have a <c>negative</c> width or height</remarks>
    /// </summary>
    public struct Size
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
    }
}
