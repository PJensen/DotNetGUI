namespace DotNetGUI.Events
{
    /// <summary>
    /// GUIVisibilityChangedEventArgs
    /// </summary>
    public class GUIVisibilityChangedEventArgs : GUIEventArgs
    {
        /// <summary>
        /// GUIVisibilityChangedEventArgs
        /// </summary>
        /// <param name="visible"></param>
        public GUIVisibilityChangedEventArgs(bool visible)
        {
            Visible = visible;
        }

        /// <summary>
        /// Visibility state
        /// </summary>
        public bool Visible { get; set; }
    }
}
