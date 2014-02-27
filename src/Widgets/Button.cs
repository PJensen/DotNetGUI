using DotNetGUI.Events;
using System;
using System.Diagnostics;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// Button
    /// </summary>
    [DebuggerDisplay("{Text}")]
    public class Button : Widget
    {
        readonly ButtonDecoration _decoration;
        readonly char _leftDecoration;
        readonly char _rightDecoration;

        /// <summary>
        /// Create a new button
        /// </summary>
        public Button(Widget parent, String text, int x, int y, ButtonDecoration decor = ButtonDecoration.SquareBracket)
            : base(text, x, y, text.Length + 2, 1, parent)
        {
            _decoration = decor;

            switch (decor)
            {
                case ButtonDecoration.AngleBracket:
                    _leftDecoration = DecorationAngleL;
                    _rightDecoration = DecorationAngleR;
                    break;

                case ButtonDecoration.Stars:
                    _leftDecoration = DecorationStar;
                    _rightDecoration = DecorationStar;
                    break;
                case ButtonDecoration.SquareBracket:
                    _leftDecoration = DecorationSquareL;
                    _rightDecoration = DecorationSquareR;
                    break;
            }

            Text = string.Format("{0}{1}{2}", _leftDecoration, text, _rightDecoration);

            EnableSelection();

            KeyboardEvent += Button_KeyboardEvent;
        }

        /// <summary>
        /// Gets the button decoration
        /// </summary>
        public ButtonDecoration Decoration
        {
            get { return _decoration; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_KeyboardEvent(object sender, GUIKeyboardEventArgs e)
        {
            switch (e.ConsoleKeyInfo.Key)
            {
                case ConsoleKey.Enter:
                    if (OkayEvent != null)
                        OkayEvent(this, new GUIEventArgs());
                    break;
            }
        }

        /// <summary>
        /// OkayEvent
        /// </summary>
        public event EventHandler<GUIEventArgs> OkayEvent;

        /// <summary>
        /// Show
        /// </summary>
        public override void Show()
        {
            base.Show();

            for (int index = 0; index < Text.Length; ++index)
            {
                // decoration text style
                if (index == 0 || index == Text.Length - 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    // normal text style
                    if (Selected)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.Write(Text[index]);
            }
        }

        /// <summary>
        /// ButtonDecoration
        /// </summary>
        public enum ButtonDecoration
        {
            SquareBracket,
            AngleBracket,
            Stars
        }

        #region Various Decoration Constants

        /// <summary>
        /// The decoration on the right of the button
        /// </summary>
        const char DecorationSquareR = ']';

        /// <summary>
        /// The decoration ont he left of the button
        /// </summary>
        const char DecorationSquareL = '[';

        /// <summary>
        /// The decoration on the right of the button
        /// </summary>
        const char DecorationAngleR = '>';

        /// <summary>
        /// The decoration ont he left of the button
        /// </summary>
        const char DecorationAngleL = '<';

        /// <summary>
        /// The decoration on the right of the button
        /// </summary>
        const char DecorationStar = '*';

        #endregion
    }
}
