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
