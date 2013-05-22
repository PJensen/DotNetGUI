﻿using DotNetGUI.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TC = ConsoleColor.Yellow;
            BG = ConsoleColor.Cyan;
            FG = ConsoleColor.Black;

            DisableSelection();
        }

        /// <summary>
        /// Show
        /// </summary>
        public override void Show()
        {
            base.Show();

            // set-up the colour scheme
            Console.ForegroundColor = FG;
            Console.BackgroundColor = BG;

            // actually perform the drawing mechanicially
            for (int index = 0; index < Width; index++)
            {
                // deliniate progress using colour.
                if (index > (Width / 100.0) * Value && Value > 0)
                    Console.BackgroundColor = TC;
                else if (Value >= 100.0)
                    Console.BackgroundColor = TC;

                // draw the text
                if (index > TextOffset)
                {
                    int offset = index - TextOffset - 1;
                    if (offset < Text.Length)
                    {
                        var tmpFG1 = Console.ForegroundColor;
                        Console.ForegroundColor = FG;
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
            get { return percentValue; }
            set
            {
                previousPercentValue = percentValue;
                percentValue = value;

                Console.Invalidate();

                
                if (Math.Abs(value - previousPercentValue) > Epsilon)
                {
                    if (ProgressChanged == null)
                        return;

                    ProgressChanged(this,
                        new GUIProgressBarEventArgs(value, previousPercentValue));
                }
            }
        }

        private const double Epsilon = 0.1;

        /// <summary>
        /// The backing store for the value
        /// </summary>
        private double percentValue;

        /// <summary>
        /// previousPercentValue
        /// </summary>
        private double previousPercentValue;

        /// <summary>
        /// Padding on both the left and right side of progress bar.
        /// </summary>
        const int Padding = 2;

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
        readonly ConsoleColor FG;

        /// <summary>
        /// the background colour
        /// </summary>
        readonly ConsoleColor BG;

        /// <summary>
        /// a third colour, for % that has been completed
        /// </summary>
        readonly ConsoleColor TC;

        /// <summary>
        /// ProgressChanged
        /// </summary>
        public event EventHandler<GUIProgressBarEventArgs> ProgressChanged;
    }
}