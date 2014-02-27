using System;
using System.Diagnostics;
using DotNetGUI.Interfaces;

namespace DotNetGUI.Attributes
{
    /// <summary>
    /// UIWidgetProperty
    /// </summary>    
    [AttributeUsage(AttributeTargets.Class)]
    public class WidgetProperties : Attribute, IColorScheme, IScreenRegion
    {
        /// <summary>
        /// Reflect
        /// </summary>
        /// <param name="widget"></param>
        internal static void Reflect(Widget widget)
        {
            foreach (var attr in widget.GetType().GetCustomAttributes(typeof(WidgetProperties), true))
            {
                Debug.Assert(typeof(WidgetProperties) == attr.GetType(), "WidgetProperties attribute type mismatch");

                widget.Location = ((WidgetProperties)attr).Location;
                widget.BackgroundColor = ((WidgetProperties)attr).BackgroundColor;
                widget.ForegroundColor = ((WidgetProperties)attr).ForegroundColor;
                widget.Size = ((WidgetProperties)attr).Size;
                widget.Text = ((WidgetProperties) attr).Text;

                return;
            }
        }

        /// <summary>
        /// Create a new WidgetProperties attribute
        /// </summary>
        /// <param name="text">the text for the widget</param>
        /// <param name="x">the x-coord</param>
        /// <param name="y">the y-coord</param>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        /// <param name="fg">the forground color (default white)</param>
        /// <param name="bg">the background color (default black)</param>
        /// <param name="visible"></param>
        public WidgetProperties(string text, int x, int y, int width, int height, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black, bool visible = true)
        {
            Text = text;
            Location = new Point(x, y);
            ForegroundColor = fg;
            BackgroundColor = bg;
            Height = height;
            Width = width;
            Visible = visible;
        }

        #region Implementation of IColorScheme

        /// <summary>
        /// the background colour
        /// </summary>
        public ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// The foreground colour
        /// </summary>
        public ConsoleColor ForegroundColor { get; set; }

        #endregion

        #region Implementation of IHasLocation

        /// <summary>
        /// The location
        /// </summary>
        public Point Location { get; set; }

        #endregion

        #region Implementation of IDimensional

        /// <summary>
        /// Widget
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Visible
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public Size Size 
        {
            get
            {
                return new Size(Width, Height);
            }
        }

        #endregion
    }
}
