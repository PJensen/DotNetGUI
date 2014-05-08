using System;
using DotNetGUI.Widgets;

namespace DotNetGUI.Sample
{
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        class MainForm : Window
        {
            /// <summary>
            /// 
            /// </summary>
            public MainForm() :
                base(new Point(0, 0), new Size(20, 15))
            {
                Title = "Testing";

                Controls.Add(new TextBox(new Point(1, 1), 10)
                    {
                        Text = "Test"
                    });
            }
        }

        static void Main(string[] args)
        {
            var mainForm = new MainForm();

            GUI.Instance.Run(mainForm);
        }
    }
}
