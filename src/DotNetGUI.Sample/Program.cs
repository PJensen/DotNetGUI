using System;
using DotNetGUI.Events;
using DotNetGUI.Widgets;

namespace DotNetGUI.Sample
{
    class Program
    {
        class MainForm : Window
        {
            public MainForm() :
                base(new Point(1, 1), new Size(50, 20))
            {
                Title = "Testing";
            }


            /// <summary>
            /// Called when [draw event].
            /// </summary>
            /// <param name="graphics">The controlbuffer.</param>
            protected override void OnDrawEvent(DisplayBuffer graphics)
            {
                base.OnDrawEvent(graphics);

                for (int i = 0; i < graphics.Width; i++)
                {
                    for (int j = 0; j < graphics.Height; j++)
                    {
                        graphics[i, j] = new Glyph('*', RandomColor, RandomColor);
                    }
                }
            }

            readonly Random random = new Random();

            ConsoleColor RandomColor
            {
                get { return (ConsoleColor)random.Next(2); }
            }

            protected override void OnInitializedEvent()
            {
                _txtZipCode = new TextBox(new Point(1, 1), 5);

                Add(_txtZipCode);

                base.OnInitializedEvent();
            }

            private TextBox _txtZipCode;
        }

        static void Main(string[] args)
        {
            GUI.Instance.Run(new MainForm());
        }
    }
}
