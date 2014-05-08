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
        ConsoleColor BG { get; set; }

        /// <summary>
        /// The primary foreground color
        /// </summary>
        ConsoleColor FG { get; set; }
    }
}
