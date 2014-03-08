using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetGUI.Widgets;

namespace DotNetGUI.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new Window(new Point(1, 1), new Size(10, 10));

            GUI.Instance.Run(window);
        }
    }
}
