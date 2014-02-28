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

        private readonly Glyph[,] _buffer;
        private readonly int _height;
        private readonly int _width;

        #endregion

        /// <summary>
        /// The width of this display buffer
        /// </summary>
        public int Width { get { return _width; } }

        /// <summary>
        /// The height of this display buffer
        /// </summary>
        public int Height { get { return _height; } }

        /// <summary>
        /// Creates a new display buffer
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public DisplayBuffer(int width, int height)
        {
            _width = width;
            _height = height;

            _buffer = new Glyph[width, height];
        }

        /// <summary>
        /// two dimensional indexer
        /// </summary>
        /// <param name="x">the x-coordinate</param>
        /// <param name="y">the y-coordinate</param>
        /// <returns></returns>
        public Glyph this[int x, int y]
        {
            get { return _buffer[x, y]; }
            set { _buffer[x, y] = value; }
        }
    }
}