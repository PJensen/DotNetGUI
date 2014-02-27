using System;

namespace DotNetGUI.Events
{
    /// <summary>
    /// GUIEventArgs
    /// </summary>
    public class GUIEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new <see cref="GUIEventArgs"/> event
        /// </summary>
        public GUIEventArgs()
        {
            ExecDt = DateTime.Now;
        }

        /// <summary>
        /// The execution date for this event.
        /// </summary>
        public DateTime ExecDt { get; set; }
    }
}
