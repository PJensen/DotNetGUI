using System;

namespace DotNetGUI.Interfaces
{
    /// <summary>
    /// IColorScheme
    /// </summary>
    public interface IColorScheme
    {
        /// <summary>
        /// the background colour
        /// </summary>
        ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// The foreground colour
        /// </summary>
        ConsoleColor ForegroundColor { get; set; }
    }
}
