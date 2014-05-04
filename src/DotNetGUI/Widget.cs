using System;
using System.Collections.Generic;

namespace DotNetGUI
{
    /// <summary>
    /// Widget
    /// </summary>
    public abstract class Widget :  IColorScheme
    {
        #region backing store

        /// <summary>
        /// The size of this widget
        /// </summary>
        private Size _size;

        /// <summary>
        /// The location of this widget
        /// </summary>
        private Point _location;

        /// <summary>
        /// The display buffer for this widget
        /// </summary>
        private readonly DisplayBuffer _displayBuffer;

        /// <summary>
        /// _controls
        /// </summary>
        private readonly List<Widget> _controls = new List<Widget>();

        /// <summary>
        /// KeyboardCallback
        /// </summary>
        public KeyboardCallback KeyboardCallback;

        #endregion

        /// <summary>
        /// Creates a new <see cref="Widget"/> at the specified location and size
        /// </summary>
        /// <param name="location">the location for this widget</param>
        /// <param name="size">the size of this widget</param>
        protected Widget(Point location, Size size)
        {
            _displayBuffer = new DisplayBuffer(size.Width, size.Height);
            Location = location;
            Size = size;
        }

        #region methods

        /// <summary>
        /// Draw
        /// </summary>
        public virtual void Draw()
        {
            OnDrawEvent(new EventArgs());
        }

        #endregion

        #region indexer

        /// <summary>
        /// index into the display buffer for this widget
        /// </summary>
        /// <param name="x">the x-coordinate</param>
        /// <param name="y">the y-coordinate</param>
        /// <returns>returns the <see cref="Glyph"/> at this location</returns>
        public Glyph this[int x, int y]
        {
            get 
            {
                return _displayBuffer[x, y];
            }
            set 
            {
                _displayBuffer[x, y] = value; 
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// TabIndex
        /// <remarks>The tab index is the order in which this and 
        /// other widgets may be tabbed through.</remarks>
        /// </summary>
        public int TabIndex { get; set; }

        /// <summary>
        /// ZIndex
        /// <remarks>The Z-index is the depth of the widget</remarks>
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// A list of widgets or controls that are sub-widgets of this one.
        /// </summary>
        public List<Widget> Controls { get { return _controls; } }

        /// <summary>
        /// The <see cref="Location"/> of this widget.
        /// </summary>
        public Point Location
        {
            get { return _location; }
            set
            {
                _location = value;
            }
        }

        /// <summary>
        /// The size of this widget
        /// </summary>
        public Size Size
        {
            get { return _size; }
            set
            {
                if (_size.Equals(value))
                    return;

                _size = value;

                OnResized(new EventArgs());
            }
        }

        /// <summary>
        /// True if this widget can be focused.
        /// </summary>
        public bool CanFocus { get; set; }

        #endregion

        #region events

        public event EventHandler Resized;
        protected void OnResized(EventArgs e)
        {
            var handler = Resized;
            if (handler != null) handler(this, e);
        }

        public event EventHandler DrawEvent;
        protected void OnDrawEvent(EventArgs e)
        {
            var handler = DrawEvent;
            if (handler != null) handler(this, e);
        }

        #endregion

        /// <summary>
        /// The default background color for this widget
        /// </summary>
        public ConsoleColor BG { get; set; }

        /// <summary>
        /// The default foreground color for this widget
        /// </summary>
        public ConsoleColor FG { get; set; }
    }
}