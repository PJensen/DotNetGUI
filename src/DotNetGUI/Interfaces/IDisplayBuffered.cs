namespace DotNetGUI.Interfaces
{
    /// <summary>
    /// IDisplayBuffered interface
    /// </summary>
    public interface IDisplayBuffered : ILocation
    {
        /// <summary>
        /// DisplayBuffer
        /// </summary>
        /// <value>
        /// The display buffer.
        /// </value>
        DisplayBuffer DisplayBuffer { get; }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <value>
        /// The <see cref="Glyph"/>.
        /// </value>
        /// <param name="x">the x-coordinate</param>
        /// <param name="y">the y-coordinate</param>
        /// <returns></returns>
        Glyph this[int x, int y] { get; set; }
    }
}