using System;

namespace DotNetGUI.Interfaces
{
    /// <summary>
    /// The color scheme interface is good for *anything* that has both
    /// a foreground and background color.
    /// </summary>
    interface IColorScheme
    {
        /// <summary>
        /// The primary background color
        /// </summary>
        /// <value>
        /// The bg.
        /// </value>
        ConsoleColor BG { get; set; }

        /// <summary>
        /// The primary foreground color
        /// </summary>
        /// <value>
        /// The fg.
        /// </value>
        ConsoleColor FG { get; set; }
    }
}
