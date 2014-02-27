using System;
using System.Diagnostics;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// TextBox
    /// </summary>
    [DebuggerDisplay("{Text}")]
    public class TextBox : Widget
    {
        /// <summary>
        /// TextBox
        /// </summary>
        /// <param name="y"></param>
        /// <param name="inputMaxLength"></param>
        /// <param name="parent"> </param>
        /// <param name="x"></param>
        public TextBox(Widget parent, int x, int y, int inputMaxLength)
            : base("", x, y, inputMaxLength, 1, parent)
        {
            MaxLength = inputMaxLength;
            EnableSelection();
            KeyboardEvent += TextBox_KeyboardEvent;
        }

        /// <summary>
        /// TextBox_KeyboardEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TextBox_KeyboardEvent(object sender, Events.GUIKeyboardEventArgs e)
        {
            if (!Visible)
                return;

            var tmpChar = e.ConsoleKeyInfo.KeyChar;

            switch (e.ConsoleKeyInfo.Key)
            {
                default:
                    if (!char.IsLetterOrDigit(tmpChar))
                        return;

                    AddNewChar(tmpChar);

                    break;

                case ConsoleKey.Backspace:
                    EraseLastChar();
                    break;

                case ConsoleKey.Enter:
                    if (OnEntered != null)
                        OnEntered(Text);
                    break;
            }
        }

        /// <summary>
        /// EraseChar
        /// </summary>
        void EraseLastChar()
        {
            if (Text.Length <= 0)
                return;

            Text = Text.Substring(0, Text.Length - 1);
            Console.Invalidate();
        }

        /// <summary>
        /// AddChar
        /// </summary>
        /// <param name="ch"></param>
        void AddNewChar(char ch)
        {
            if (Text.Length + 1 > MaxLength)
                return;

            Text += ch;

            Console.Invalidate();
        }

        /// <summary>
        /// Show
        /// </summary>
        public override void Show()
        {
            const char defaultChar = ' ';

            base.Show();

            Console.BackgroundColor = Selected ? ConsoleColor.DarkGray : ConsoleColor.Black;

            for (var index = 0; index < MaxLength; ++index)
                Console.Write(index < Text.Length ? Text[index] : defaultChar);
        }

        /// <summary>
        /// OnEntered some Text
        /// </summary>
        public Action<string> OnEntered { get; set; }

        /// <summary>
        /// MaxLength
        /// </summary>
        public int MaxLength { get; protected set; }
    }
}
