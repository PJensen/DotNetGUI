using DotNetGUI.Events;
using System;
using System.Diagnostics;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// ProgressBar
    /// </summary>
    [DebuggerDisplay("{Value}")]
    public class ProgressBar : Widget
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="v"></param>
        public ProgressBar(Widget parent, string text, int x, int y, double v = 0.0)
            : base(text, x, y, DefaultWidth, 1, parent)
        {
            Value = v;

            // init progress-bar colour scheme
            _tc = ConsoleColor.Yellow;
            _bg = ConsoleColor.Cyan;
            _fg = ConsoleColor.Black;

            DisableSelection();
        }

        /// <summary>
        /// Show
        /// </summary>
        public override void Show()
        {
            base.Show();

            // set-up the colour scheme
            Console.ForegroundColor = _fg;
            Console.BackgroundColor = _bg;

            // actually perform the drawing mechanicially
            for (int index = 0; index < Width; index++)
            {
                // deliniate progress using colour.
                if (index > (Width / 100.0) * Value && Value > 0)
                    Console.BackgroundColor = _tc;
                else if (Value >= 100.0)
                    Console.BackgroundColor = _tc;

                // draw the text
                if (index > TextOffset)
                {
                    int offset = index - TextOffset - 1;
                    if (offset < Text.Length)
                    {
                        var tmpFG1 = Console.ForegroundColor;
                        Console.ForegroundColor = _fg;
                        Console.Write(Text[offset]);
                        Console.ForegroundColor = tmpFG1;
                    }
                }

                Console.SetCursorPosition(index, 0);

                // write a specific charcter  to the screen
                //  '    running     '
                Console.Write(DisplayGlyph);
            }
        }

        /// <summary>
        /// the text offset is used in positioning calculations.
        /// </summary>
        int TextOffset { get { return ((Width / 2)) - (Text.Length / 2); } }

        /// <summary>
        /// The current percentage as represented by this progress bar.
        /// </summary>
        public double Value
        {
            get { return _percentValue; }
            set
            {
                _previousPercentValue = _percentValue;
                _percentValue = value;

                Console.Invalidate();


                if (!(Math.Abs(value - _previousPercentValue) > Epsilon)) return;

                if (ProgressChanged == null)
                    return;

                ProgressChanged(this, new GUIProgressBarEventArgs(value, _previousPercentValue));
            }
        }

        private const double Epsilon = 0.1;

        /// <summary>
        /// The backing store for the value
        /// </summary>
        private double _percentValue;

        /// <summary>
        /// previousPercentValue
        /// </summary>
        private double _previousPercentValue;

        /// <summary>
        /// the default width for any progress bar.
        /// </summary>
        const int DefaultWidth = 20;

        /// <summary>
        /// the character used for displaying percentage complete
        /// </summary>
        const char DisplayGlyph = ' ';

        /// <summary>
        /// foreground colour, generally text for a progress bar
        /// </summary>
        readonly ConsoleColor _fg;

        /// <summary>
        /// the background colour
        /// </summary>
        readonly ConsoleColor _bg;

        /// <summary>
        /// a third colour, for % that has been completed
        /// </summary>
        readonly ConsoleColor _tc;

        /// <summary>
        /// ProgressChanged
        /// </summary>
        public event EventHandler<GUIProgressBarEventArgs> ProgressChanged;
    }
}
