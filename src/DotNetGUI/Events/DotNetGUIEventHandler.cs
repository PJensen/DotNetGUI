using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetGUI.Events
{
    /// <summary>
    /// DotNetGUIEventHandler
    /// </summary>
    /// <param name="sender">the event sender</param>
    /// <param name="widget"></param>
    /// <returns></returns>
    public delegate void DotNetGUIEventHandler(object sender, Widget widget);
}
