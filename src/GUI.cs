﻿using DotNetGUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DotNetGUI
{
    /// <summary>
    /// GUI
    /// </summary>
    public class GUI
    {
        /// <summary>
        /// done
        /// </summary>
        volatile bool done = false;

        /// <summary>
        /// inputInteruppted
        /// </summary>
        volatile bool inputInteruppted = false;

        /// <summary>
        /// Run
        /// </summary>
        public void Run(Widget root)
        {
            Console.Title = root.Text;

            StartKeyboardInputThread();

            root.InitializeWidget();
            root.Show();

            DrawWidget(root);

            while (!done)
            {
                PushCursorState();

                // critical section
                lock (SyncRoot)
                {
                    Widget.Traverse(DrawWidget, root, w => w.Console.Invalidated && w.Visible);
                }

                PopAndSetCursorState();
            }
        }

        /// <summary>
        /// consoleSize
        /// </summary>
        readonly static Size ConsoleSize = new Size(Console.WindowWidth, Console.WindowHeight);

        /// <summary>
        /// StartKeyboardInputThread
        /// </summary>
        private void StartKeyboardInputThread()
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                while (!done)
                {
                    if (Console.KeyAvailable && !inputInteruppted)
                    {
                        KeyboardCallback(Console.ReadKey(true));
                    }

                    Thread.Sleep(10);
                }
            });
        }

        /// <summary>
        /// DrawWidget
        /// </summary>
        /// <param name="w">the widget to draw</param>
        public static void DrawWidget(Widget w)
        {
            IPoint screenLocation = w.Location;

            w.Show();

            for (var y = 0; y <= w.Console.Height; ++y)
            {
                for (var x = 0; x <= w.Console.Width; ++x)
                {
                    var g = w.Console[x, y];

                    if (Buffer[screenLocation.X + x, screenLocation.Y + y] == g) continue;

                    Console.SetCursorPosition(screenLocation.X + x, screenLocation.Y + y);
                    Console.ForegroundColor = g.FG;
                    Console.BackgroundColor = g.BG;
                    Console.Write(g.G);

                    Buffer[x, y] = g;
                }
            }

            w.Console.Validate();
        }

        /// <summary>
        /// DisplayBuffer
        /// </summary>
        static GUI()
        {
            CursorStateStack = new Stack<Utility.CursorState>();
            Buffer = new DisplayBuffer(ConsoleSize);
        }

        /// <summary>
        /// Buffer
        /// </summary>
        internal static DisplayBuffer Buffer { get; set; }

        /// <summary>
        /// KeyboardCallback
        /// </summary>
        public event Action<ConsoleKeyInfo> KeyboardCallback;

        /// <summary>
        /// ScreenCenter
        /// <value>The location of the center of the screen.</value>
        /// </summary>
        public static Point ScreenCenter
        {
            get
            {
                return new Point(Console.WindowWidth / 2,
                    Console.WindowHeight / 2);
            }
        }

        /// <summary>
        /// ScreenWidth
        /// </summary>
        public static int ScreenWidth { get { return Console.WindowWidth; } }

        /// <summary>
        /// ScreenHeight
        /// </summary>
        public static int ScreenHeight { get { return Console.WindowHeight; } }

        /// <summary>
        /// ScreenSize
        /// </summary>
        public static Size ScreenSize { get { return ConsoleSize; } }

        /// <summary>
        /// Return the screen center relative to the passed size
        /// </summary>
        /// <param name="size">the size</param>
        /// <returns>the screen center +/- 1</returns>
        public static Point SizeToScreenCenter(Size size)
        {
            return new Point(ScreenWidth / 2 - size.Width / 2, ScreenHeight / 2 - size.Height / 2);
        }

        #region Cursor State Stack

        /// <summary>
        /// Push the cursor state
        /// </summary>
        public static void PushCursorState()
        {
            CursorStateStack.Push(CurrentCursorState);
        }

        /// <summary>
        /// Pops the cursor state
        /// </summary>
        public static void PopAndSetCursorState()
        {
            if (CursorStateStack.Count <= 0)
                return;

            CursorStateStack.Pop().Set();
        }

        /// <summary>
        /// CursorStateStack
        /// </summary>
        static Stack<Utility.CursorState> CursorStateStack { get; set; }

        /// <summary>
        /// Returns the current cursor state as known by the system Console object.
        /// </summary>
        public static Utility.CursorState CurrentCursorState
        {
            get
            {
                return new Utility.CursorState(Console.CursorLeft, Console.CursorTop,
                    Console.ForegroundColor, Console.BackgroundColor);
            }
        }

        #endregion

        #region Singleton

        /// <summary>
        /// instance
        /// </summary>
        private static volatile GUI _instance;

        /// <summary>
        /// syncRoot
        /// </summary>
        private static readonly object SyncRoot = new Object();

        /// <summary>
        /// GUI Constructor
        /// </summary>
        private GUI() { }

        /// <summary>
        /// Instance
        /// </summary>
        public static GUI Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new GUI();
                    }
                }

                return _instance;
            }
        }

        #endregion
    }
}
