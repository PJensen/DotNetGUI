using System;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// Window
    /// </summary>
    public class Window : Widget
    {
        /// <summary>
        /// The the title for this window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public Window(Point location, Size size)
            : base(location, size)
        {
            base.BG = ConsoleColor.Blue;
            base.FG = ConsoleColor.White;

            KeyboardCallback += delegate(ConsoleKeyInfo info)
            {
                switch (info.Key)
                {
                    case ConsoleKey.Escape:
                        GUI.Instance.Exit();
                        break;
                }
            };
        }

        /// <summary>
        /// Draw
        /// </summary>
        public sealed override void Draw()
        {
            #region fill

            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    base[x, y] = new Glyph()
                    {
                        BG = BG,
                        FG = FG,
                        G = ' ',
                    };
                }
            }

            #endregion

            #region left vertical bar

            for (int x = 0; x < Size.Width; x++)
            {
                base[0, x] = new Glyph()
                {
                    BG = base.BG,
                    FG = base.FG,
                    G = '|',
                };
            } 

            #endregion

            #region right vertical bar

            for (int x = 0; x < Size.Width; x++)
            {
                base[Size.Width - 1, x] = new Glyph()
                {
                    BG  = base.BG,
                    FG = base.FG,
                    G = 'x',
                };
            } 

            #endregion

            #region top horizontal bar

            for (int x = 0; x < Size.Height; x++)
            {
                base[x, 0] = new Glyph()
                {
                    BG  = base.BG,
                    FG = base.FG,
                    G = 'x',
                };
            } 

            #endregion

            #region bottom horizontal bar

            for (int y = 0; y < Size.Height; y++)
            {
                base[y, Size.Height - 1] = new Glyph()
                {
                    BG  = base.BG,
                    FG = base.FG,
                    G = '=',
                };
            } 

            #endregion

            base.Draw();
        }
    }
}
