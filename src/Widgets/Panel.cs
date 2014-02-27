using DotNetGUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// Panel
    /// </summary>
    public class Panel : Widget
    {
        /// <summary>
        /// Creates a new panel
        /// </summary>
        /// <param name="parent"> </param>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="width">the width of this panel</param>
        /// <param name="height">the height of this panel</param>
        public Panel(Widget parent, int x, int y, int width, int height)
            : base(x, y, width, height)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Panel(IPoint p, int width, int height)
            : base(p.X, p.Y, width, height)
        {
        }

        /// <summary>
        /// Panel
        /// </summary>
        /// <param name="r">The screen region</param>
        public Panel(IScreenRegion r)
            : this(r.Location, r.Width, r.Height)
        { 
        }
    }
}
