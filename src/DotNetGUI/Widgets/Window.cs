using System;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// Window
    /// </summary>
    public class Window : Widget
    {
        /// <summary>
        /// The the title for this window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public Window(Point location, Size size)
            : base(location, size)
        {
            KeyboardCallback += delegate(ConsoleKeyInfo info)
            {
                switch (info.Key)
                {
                    case ConsoleKey.Escape:
                        GUI.Instance.Exit();
                        break;
                }
            };
        }
    }
}
