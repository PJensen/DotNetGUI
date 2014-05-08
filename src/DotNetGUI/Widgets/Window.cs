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
        /// Createa a new window at the specified point and location
        /// </summary>
        /// <param name="location">the location</param>
        /// <param name="size">the size</param>
        public Window(Point location, Size size)
            : base(location, size)
        {
            BG = ConsoleColor.Blue;
            FG = ConsoleColor.White;

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

            var width = Size.Width;
            var height = Size.Height;

            for (var x = 1; x < width - 1; x++)
            {
                for (var y = 1; y < height - 1; y++)
                {
                    base[x, y] = new Glyph
                        {
                            BG = BG,
                            FG = FG,
                            G = ' ',
                        };
                }
            }

            #endregion

            #region left vertical bar

            for (var x = 0; x < Size.Height; x++)
            {
                base[0, x] = new Glyph
                    {
                        BG = BG,
                        FG = FG,
                        G = '║',
                    };

                base[Size.Width - 1, x] = new Glyph
                {
                    BG = BG,
                    FG = FG,
                    G = '║',
                };
            }

            #endregion

            #region top horizontal bar

            for (var x = 0; x < Size.Width; x++)
            {
                base[x, 0] = new Glyph
                    {
                        BG = BG,
                        FG = FG,
                        G = x == 0 ? '╔' : x >= Size.Width - 1 ? '╗' : '═',
                    };
            }

            #endregion

            #region bottom horizontal bar

            for (var y = 0; y < Size.Width; y++)
            {
                base[y, Size.Height - 1] = new Glyph
                    {
                        BG = BG,
                        FG = FG,
                        G = y == 0 ? '╚' : y >= Size.Width - 1 ? '╝' : '═',
                    };
            }

            #endregion

            #region draw title

            var xOffset = CenterOffsetX(Title.Length);

            for (var j = 0; j < Title.Length; j++)
            {
                base[j + xOffset, 0] = new Glyph(Title[j], FG, BG);
            }

            #endregion

            base.Draw();
        }
    }
}
