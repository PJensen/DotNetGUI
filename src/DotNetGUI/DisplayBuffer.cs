using DotNetGUI.Interfaces;

namespace DotNetGUI
{
    /// <summary>
    /// A <see cref="DisplayBuffer"/> is an offscreen buffer with a width and height
    /// that closely mirrors the contents of a screen except it's not the screen 
    /// at all.
    /// <remarks>once a display buffer is created it's size cannot be changed</remarks>
    /// </summary>
    public class DisplayBuffer
    {
        #region backing store

        private readonly byte _width;
        private readonly byte _height;
        private readonly Glyph[,] _glyphs;

        #endregion

        /// <summary>
        /// Initializes a new <see cref="DisplayBuffer"/>
        /// </summary>
        /// <param name="width">the width of the glyph buffer</param>
        /// <param name="height">the height of the glyph buffer</param>
        public DisplayBuffer(int width, int height)
        {
            _width = (byte)width;
            _height = (byte)height;
            _glyphs = new Glyph[width, height];
        }

        /// <summary>
        /// The height of the buffer
        /// </summary>
        public int Height { get { return _height; } }

        /// <summary>
        /// The width of the buffer
        /// </summary>
        public int Width { get { return _width; } }

        /// <summary>
        /// Glyph two dimensional indexer
        /// </summary>
        /// <param name="x">the x-coordinate</param>
        /// <param name="y">the y-coordinate</param>
        /// <returns>the glyph at the specified location</returns>
        public Glyph this[int x, int y]
        {
            get { return _glyphs[x, y]; }
            set { _glyphs[x, y] = value; }
        }

        /// <summary>
        /// MergeDisplayBuffers
        /// 1. Actually perform the drawing step of the widget passed as an argument.
        /// 2. Iterate over it's display buffer; setting the _secondaryBuffer - will later be used from comparison.
        /// 3. Recurse with any controls that are attached; ordered by z-index descending.
        /// <remarks>Merge all display buffers down to the secondary display buffer</remarks>
        /// </summary>
        /// <returns></returns>
        internal void MergeDownDisplayBuffers(Size size, Point offset, IDisplayBuffered source)
        {
            for (var y = 0; y < size.Height; y++)
            {
                for (var x = 0; x < size.Width; x++)
                {
                    this[x + offset.X, y + offset.Y] = source[x, y];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        internal void MergeDownDisplayBuffers(IDisplayBuffered source)
        {
            for (var y = 0; y < source.DisplayBuffer.Height; y++)
            {
                for (var x = 0; x < source.DisplayBuffer.Width; x++)
                {
                    this[x + source.Location.X, y + source.Location.Y] = source[x, y];
                }
            }
        }
    }
}