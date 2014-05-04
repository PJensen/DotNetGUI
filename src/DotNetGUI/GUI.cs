using System;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace DotNetGUI
{
    #region GUI delegates

    /// <summary>
    /// KeyboardCallback delegate
    /// </summary>
    /// <param name="keyInfo">the keypress info</param>
    public delegate void KeyboardCallback(ConsoleKeyInfo keyInfo);

    #endregion

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
                        _instance = new GUI();
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
        private const int DisplayThreadSleepWait = 10;

        #endregion

        #region backing store

        /// <summary>
        /// exit
        /// </summary>
        private volatile bool _done;

        /// <summary>
        /// The primary display buffer
        /// </summary>
        private readonly DisplayBuffer _primaryBuffer = new DisplayBuffer(Console.WindowWidth, Console.WindowHeight);

        /// <summary>
        /// The secondary display buffer
        /// </summary>
        private readonly DisplayBuffer _secondaryBuffer = new DisplayBuffer(Console.WindowWidth, Console.WindowHeight);

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

            CursorState.Pop();
        }

        /// <summary>
        /// Start the keyboard callback thread
        /// </summary>
        private void StartKeyboardCallBackThread()
        {
            var methodName = new StackFrame().GetMethod().Name;

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
            var methodName = new StackFrame().GetMethod().Name;

            Debug.Write(" [" + Thread.CurrentThread.ManagedThreadId + "] " + methodName + " ... ");

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

        /// <summary>
        /// MergeDisplayBuffers
        /// 1. Actually perform the drawing step of the widget passed as an argument.
        /// 2. Iterate over it's display buffer; setting the _secondaryBuffer - will later be used from comparison.
        /// 3. Recurse with any controls that are attached; ordered by z-index descending.
        /// <remarks>Merge all display buffers down to the secondary display buffer</remarks>
        /// </summary>
        /// <returns></returns>
        private void MergeDownDisplayBuffers(Widget widget)
        {
            // set the x and y offset for writing into the display buffer
            var xOffset = widget.Location.X;
            var yOffset = widget.Location.Y;


            widget.Draw();

            for (int y = 0; y < widget.Size.Height; y++)
            {
                for (int x = 0; x < widget.Size.Width; x++)
                {
                    _secondaryBuffer[x + xOffset, y + yOffset] = widget[x, y];
                }
            }

            foreach (var control in widget.Controls.OrderByDescending(w => w.ZIndex))
            {
                MergeDownDisplayBuffers(control);
            }
        }

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
            MergeDownDisplayBuffers(widget);

            lock (widget)
            {
                for (int y = 0; y < _primaryBuffer.Height - 1; y++)
                {
                    bool[] redrawColumns = new bool[_primaryBuffer.Width];

                    for (int x = 0; x < _primaryBuffer.Width - 1; x++)
                    {
                        redrawColumns[x] = _secondaryBuffer[x, y] != _primaryBuffer[x, y];
                    }

                    if (redrawColumns.All(x => !x))
                    {
                        continue;
                    }

                    for (int x = 0; x < _primaryBuffer.Width - 1; x++)
                    {
                        _primaryBuffer[x, y] = _secondaryBuffer[x, y];
                    }

                    Console.CursorTop = y;

                    for (int x = 0; x < _primaryBuffer.Width - 1; x++)
                    {
                        if (redrawColumns[x])
                        {
                            Console.CursorLeft = x;
                            Console.BackgroundColor = _primaryBuffer[x, y].BG;
                            Console.ForegroundColor = _primaryBuffer[x, y].FG;

                            Console.Write(_primaryBuffer[x, y].G);
                        }
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
