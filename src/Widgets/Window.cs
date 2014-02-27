using System;
using System.Diagnostics;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// Window
    /// </summary>
    [DebuggerDisplay("{Title}")]
    public class Window : Widget
    {
        /// <summary>
        /// Creates a centered root level window
        /// </summary>
        /// <param name="text">the text</param>
        /// <param name="size">the size</param>
        /// <param name="parent"> </param>
        public Window(string text, Size size, Widget parent = null)
            : base(text, GUI.SizeToScreenCenter(size), size, parent)
        {
            KeyboardEvent += Window_KeyboardEvent;
        }

        /// <summary>
        /// Window
        /// </summary>
        /// <param name="parent"></param>
        public Window(Widget parent = null)
            : base(parent)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_KeyboardEvent(object sender, Events.GUIKeyboardEventArgs e)
        {
            switch (e.ConsoleKeyInfo.Key)
            {
                case ConsoleKey.Escape:
                    Dispose();            
                    break;
            }
        }

        /// <summary>
        /// Show
        /// </summary>
        public override void Show()
        {
            base.Show();

            Box(Title, 0, 0, Width, Height);
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get { return Text; } set { Text = value; } }
    }
}
