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
                base(new Point(1, 1), new Size(20, 15))
            {
                Title = "Testing";
            }
        }

        static void Main(string[] args)
        {
            var mainForm = new MainForm()
            {
                new TextBox(new Point(1,1), 10)
                {
                    Text = "One",
                    KeyboardCallback = delegate(ConsoleKeyInfo info) {  }
                }
            };

            GUI.Instance.Run(mainForm);
        }
    }
}
