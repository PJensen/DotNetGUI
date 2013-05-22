using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DotNetGUI.Attributes;
using DotNetGUI.Events;
using DotNetGUI.Widgets;
using DotNetGUI;

namespace ConsoleTest
{
    class Program
    {
        /*
        [WidgetProperties("Text", 1, 1, 10, 10)]
        private class MainFrame : Window
        {
        }
        */

        static void Main(string[] args)
        {
            //var mainFrame = new MainFrame();
            //mainFrame.InitializeWidget();

            var main = new Window("Main", new Size(25, 12));
            
            var textBox1 = new TextBox(main, 11, 2, 10);
            var textBox2 = new TextBox(main, 11, 4, 10);
            var checkBox1 = new Checkbox(main, 2, 6, "Enabled");
            var progressBar1 = new ProgressBar(main, "Testing", 2, 8);
            var button1 = new Button(main, "OK", 17, 10, Button.ButtonDecoration.AngleBracket);
            button1.OkayEvent += delegate { Environment.Exit(0); };
            var button2 = new Button(main, "Reset", 8, 10);
            var buttonPlus = new Button(main, "+", 3, 10);
            var buttonMinus = new Button(main, "-", 3, 11);
            buttonPlus.OkayEvent += delegate { if (!checkBox1.Checked) return; progressBar1.Value++; };
            buttonMinus.OkayEvent += delegate { if (!checkBox1.Checked) return; progressBar1.Value--; };
            button2.OkayEvent += delegate { if (!checkBox1.Checked) return; progressBar1.Value = 0; };
            progressBar1.ProgressChanged += delegate { textBox2.Text = progressBar1.Value.ToString(CultureInfo.InvariantCulture); textBox2.Console.Invalidate(); };
            textBox1.OnEntered += delegate(string s)
                                      {
                                          if (!checkBox1.Checked) return;
                                          try { progressBar1.Value = int.Parse(s); }
                                          catch (Exception) { }
                                      };
            checkBox1.OnChecked += (sender, eventArgs) => { };
            main.Add(textBox1);
            main.Add(textBox2);
            main.Add(checkBox1);
            main.Add(new Label(main, "Percent:", textBox1));
            main.Add(progressBar1);
            main.Add(button1);
            main.Add(button2);
            main.Add(buttonPlus);
            main.Add(buttonMinus);
            main.InitializeWidget();
            GUI.Instance.Run(main);
        }
    }
}
