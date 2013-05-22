using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// Checkbox
    /// </summary>
    public class Checkbox : Widget
    {
        /// <summary>
        /// Checkbox
        /// </summary>
        public Checkbox(Widget parent, int x, int y, string text, bool @checked = false)
            : base(text, x, y, text.Length + 4, 1, parent)
        {
            _checked = @checked;

            KeyboardEvent += Checkbox_KeyboardEvent;
            OnChecked += Checkbox_OnChecked;
            EnableSelection();
        }

        /// <summary>
        /// Checkbox_OnChecked
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        void Checkbox_OnChecked(object sender, EventArgs e)
        {
            Console.Invalidate();
        }

        /// <summary>
        /// Checkbox_KeyboardEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Checkbox_KeyboardEvent(object sender, Events.GUIKeyboardEventArgs e)
        {
            switch (e.ConsoleKeyInfo.Key)
            {
                default:
                    return;
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    Checked = !Checked;
                    break;
            }
        }

        /// <summary>
        /// Show
        /// </summary>
        public override void Show()
        {
            base.Show();
            
            Console.SetCursorPosition(0, 0);
            Console.Write(Text);
            Console.ForegroundColor = Selected ? ConsoleColor.Gray : ConsoleColor.White;
            Console.Write(" [");
            Console.ForegroundColor = Checked ? ConsoleColor.Green : ConsoleColor.DarkYellow;
            Console.Write(Checked ? '■' : '∙');
            Console.ForegroundColor = Selected ? ConsoleColor.Gray : ConsoleColor.White;
            Console.Write("]");
        }

        /// <summary>
        /// Checked
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set
            {
                if (_checked != value && OnChecked != null)
                    OnChecked(this, new EventArgs());
                _checked = value;
            }
        }

        /// <summary>
        /// Checked
        /// </summary>
        private bool _checked = false;

        /// <summary>
        /// OnChecked
        /// </summary>
        public event EventHandler OnChecked;
    }
}
