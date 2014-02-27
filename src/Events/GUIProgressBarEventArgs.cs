namespace DotNetGUI.Events
{
    /// <summary>
    /// GUIEventProgressBarEventArgs
    /// </summary>
    public class GUIProgressBarEventArgs : GUIEventArgs
    {
        /// <summary>
        /// GUIEventProgressBarEventArgs
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="prevValue"></param>
        public GUIProgressBarEventArgs(double newValue, double prevValue)
        {
            NewValue = newValue;
            PreviousValue = prevValue;
        }

        /// <summary>
        /// NewValue
        /// </summary>
        public double PreviousValue { get; private set; }

        /// <summary>
        /// NewValue
        /// </summary>
        public double NewValue { get; private set; }
    }
}
