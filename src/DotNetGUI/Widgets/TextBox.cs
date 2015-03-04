using System;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// TextBox
    /// </summary>
    public sealed class TextBox : Widget
    {
        #region constants

        private const ConsoleColor DefaultForegroundColor = ConsoleColor.White;
        private const ConsoleColor DefaultBackgroundColor = ConsoleColor.Gray;

        #endregion

        #region backing store
        
        private string _text; 

        #endregion

        /// <summary>
        /// TextBox
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="width">The width.</param>
        public TextBox(Point location, int width)
            : base(location, new Size(width, 1))
        {
            FG = DefaultForegroundColor;
            BG = DefaultBackgroundColor;
        }

        /// <summary>
        /// Text
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                {
                    return;
                }
                
                _text = value;

                OnTextChanged();
            }
        }

        /// <summary>
        /// Draw the text box
        /// </summary>
        public override void Draw()
        {  
            for (var x = 0; x < Size.Width; x++)
            {
                if (Text != null && x < Text.Length)
                {
                    base[x, 0] = new Glyph(Text[x], FG, BG);
                }
                else
                {
                    base[x, 0] = new Glyph(' ', FG, BG);
                }
            }

            base.Draw();
        }

        /// <summary>
        /// Text change event handler
        /// </summary>
        public event EventHandler TextChanged;

        /// <summary>
        /// Text change event invocator
        /// </summary>
        private void OnTextChanged()
        {
            var handler = TextChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
