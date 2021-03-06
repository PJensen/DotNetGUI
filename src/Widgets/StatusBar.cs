﻿using System;

namespace DotNetGUI.Widgets
{
    public class StatusBar : Widget
    {
        /// <summary>
        /// StatusBar
        /// </summary>
        /// <param name="parent"></param>
        public StatusBar(Widget parent)
            : base("", 0, parent.Height, parent.Width, 1)
        {
            //Widgets.Add(new Label(parent, "testing 1234"));
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Show
        /// </summary>
        public override void Show()
        {
            base.Show();

            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(this);
            for (int index = 0; index < Width; ++index)
                Console.Write(' ');
        }
    }
}
