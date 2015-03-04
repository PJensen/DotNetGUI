﻿using System;
using System.Collections.Generic;

namespace DotNetGUI.Widgets
{
    /// <summary>
    /// Window
    /// </summary>
    public abstract class Window : Widget, IEnumerable<Widget>
    {
        /// <summary>
        /// The the title for this window
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Createa a new window at the specified point and location
        /// </summary>
        /// <param name="location">the location</param>
        /// <param name="size">the size</param>
        protected Window(Point location, Size size)
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

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Widget> GetEnumerator()
        {
            return ((IEnumerable<Widget>) Controls).GetEnumerator();
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds the widget to this widget
        /// </summary>
        /// <param name="widget">The widget.</param>
        public void Add(Widget widget)
        {
            Controls.Add(widget);
        }
    }
}
