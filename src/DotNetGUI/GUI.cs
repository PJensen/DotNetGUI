using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using DotNetGUI.Events;

namespace DotNetGUI
{
    /// <summary>
    /// The <see cref="GUI"/> static class can be used for 
    /// perform higher level GUI operations on various widgets.
    /// <remarks>This is the connection point to most if not all system calls.</remarks>
    /// </summary>
    public class GUI
    {
        #region threadsafe lazy singleton

        /// <summary>
        /// the singleton instance of the GUI
        /// </summary>
        private static volatile GUI _instance;

        /// <summary>
        /// The sync root for locking
        /// </summary>
        private static readonly object SyncRoot = new Object();

        /// <summary>
        /// private constructor
        /// </summary>
        private GUI()
        {
        }

        /// <summary>
        /// Singleton accessor
        /// </summary>
        public static GUI Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new GUI();
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region constants

        /// <summary>
        /// The sleep time between keyboard events
        /// </summary>
        private const int KeyboardThreadSleepWait = 10;

        /// <summary>
        /// The sleep time between GUI draw events
        /// </summary>
        private const int DisplayThreadSleepWait = 5;

        #endregion

        #region backing store

        /// <summary>
        /// The primary display buffer
        /// </summary>
        private readonly DisplayBuffer _primaryBuffer = new DisplayBuffer(Console.WindowWidth, Console.WindowHeight);

        /// <summary>
        /// The secondary display buffer
        /// </summary>
        private readonly DisplayBuffer _secondaryBuffer = new DisplayBuffer(Console.WindowWidth, Console.WindowHeight);

        /// <summary>
        /// exit
        /// </summary>
        private volatile bool _done;

        #endregion

        #region events and callbacks

        /// <summary>
        /// The main keyboard callback.
        /// </summary>
        private KeyboardCallback _keyboardCallback;

        #endregion

        #region methods

        /// <summary>
        /// Run's a DotNetGUI program given the root widget.
        /// </summary>
        /// <param name="widget"></param>
        public void Run(Widget widget)
        {
            CursorState.Push();

            StartKeyboardCallBackThread();

            lock (widget)
            {
                _keyboardCallback += widget.KeyboardCallback;

                StartDrawThread(widget);
            }

            Thread.Sleep(Timeout.Infinite);

            CursorState.Pop().Set();
        }

        /// <summary>
        /// Start the keyboard callback thread
        /// </summary>
        private void StartKeyboardCallBackThread()
        {
            string methodName = new StackFrame().GetMethod().Name;

            Debug.Write(" [" + Thread.CurrentThread.ManagedThreadId + "] " + methodName + " ... ");

            ThreadPool.QueueUserWorkItem(delegate
                                             {
                                                 while (!_done)
                                                 {
                                                     Thread.Sleep(KeyboardThreadSleepWait);

                                                     if (_keyboardCallback == null || !Console.KeyAvailable) continue;

                                                     lock (SyncRoot)
                                                     {
                                                         _keyboardCallback(Console.ReadKey(true));
                                                     }
                                                 }
                                             });
        }

        /// <summary>
        /// Start the main draw thread
        /// </summary>
        private void StartDrawThread(Widget widget)
        {
            string methodName = new StackFrame().GetMethod().Name;

            Debug.Write(" [" + Thread.CurrentThread.ManagedThreadId + "] " + methodName + " ... ");

            widget.Initialize();

            ThreadPool.QueueUserWorkItem(delegate
                                             {
                                                 while (!_done)
                                                 {
                                                     Thread.Sleep(DisplayThreadSleepWait);

                                                     lock (SyncRoot)
                                                     {
                                                         DrawWidget(widget);
                                                     }
                                                 }
                                             });
        }


        /*
         *             foreach (var control in widget.Controls.OrderByDescending(w => w.ZIndex))
            {
                MergeDownDisplayBuffers(control, displayBuffer);
            } */

        /// <summary>
        /// 1. Merge down display buffers for the whole screen.
        /// 2. Create a "redrawColumns" array and set it acording to changes from the primary
        ///    buffer and the secondary buffer
        /// 3. If all bits in the array are false; skip on to the next row.
        /// 4. Set the cursorTop to be the y-coordinate (expensive)
        /// 5. Loop through columns; only setting x-coordinate when a change is detected (again primary & secondary)
        ///    draw the G (char) with the specified foreground and background color.
        /// <remarks>The recursion is done in the <see cref="MergeDownDisplayBuffers"/> step</remarks>
        /// </summary>
        /// <param name="widget"></param>
        private void DrawWidget(Widget widget)
        {
            widget.Draw();

            _secondaryBuffer.MergeDownDisplayBuffers(widget);

            lock (widget)
            {
                for (var y = 0; y < _primaryBuffer.Height - 1; y++)
                {
                    var redrawColumns = new bool[_primaryBuffer.Width];

                    for (var x = 0; x < _primaryBuffer.Width - 1; x++)
                    {
                        redrawColumns[x] = _secondaryBuffer[x, y] != _primaryBuffer[x, y];
                    }

                    if (redrawColumns.All(x => !x))
                    {
                        continue;
                    }

                    for (var x = 0; x < _primaryBuffer.Width - 1; x++)
                    {
                        _primaryBuffer[x, y] = _secondaryBuffer[x, y];
                    }

                    Console.CursorTop = y;

                    for (var x = 0; x < _primaryBuffer.Width - 1; x++)
                    {
                        if (!redrawColumns[x]) continue;

                        Console.CursorLeft = x;

                        var glyph = _primaryBuffer[x, y];

                        if (glyph.BG != Console.BackgroundColor)
                        {
                            Console.BackgroundColor = glyph.BG;
                        }

                        if (glyph.FG != Console.ForegroundColor)
                        {
                            Console.ForegroundColor = glyph.FG;
                        }

                        Console.Write(glyph.G);
                    }
                }
            }
        }

        /// <summary>
        /// Exit
        /// </summary>
        public void Exit()
        {
            _done = true;
        }

        #endregion
    }
}