using System;
using DotNetGUI.Widgets;

namespace DotNetGUI.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new Window(new Point(0, 0), new Size(12, 13))
            {
                BG = ConsoleColor.Blue,
                FG = ConsoleColor.White,
                Title = "Testing",
            };

            GUI.Instance.Run(window);
        }
    }
}
