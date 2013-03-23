using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetGUI.Attributes
{
    /// <summary>
    /// UIWidgetProperty
    /// </summary>
    public class GUIProperty : Attribute
    {
        int Width { get; set; }
        int Height { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}
