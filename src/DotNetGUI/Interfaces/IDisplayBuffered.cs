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
        DisplayBuffer DisplayBuffer { get; }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="x">the x-coordinate</param>
        /// <param name="y">the y-coordinate</param>
        /// <returns></returns>
        Glyph this[int x, int y] { get; set; }
    }
}