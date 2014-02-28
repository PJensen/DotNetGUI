using System;

namespace DotNetGUI
{
    /// <summary>
    /// 
    /// </summary>
    public class Widget
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public Widget(Point location, Size size)
        {
            Location = location;
            Size = size;
        }

        /// <summary>
        /// TabIndex
        /// </summary>
        public int TabIndex { get; set; }

        /// <summary>
        /// ZIndex
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// 
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
        /// 
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

        public bool CanFocus { get; set; }
        private Size _size;
        private Point _location;
        private readonly DisplayBuffer displayBuffer;



        #region events

        public event EventHandler Resized;
        public void OnResized(EventArgs e)
        {
            var handler = Resized;
            if (handler != null) handler(this, e);
        }

        #endregion
    }
}