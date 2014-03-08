using System;
using System.Diagnostics;
using System.Threading;

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

        #endregion

        #region backing store

        /// <summary>
        /// exit
        /// </summary>
        private volatile bool _done;

        #endregion

        /// <summary>
        /// The main keyboard callback.
        /// </summary>
        private KeyboardCallback _keyboardCallback;

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
            }

            Thread.Sleep(30000);

            CursorState.Pop();
        }

        /// <summary>
        /// StartKeyboardCallBackThread
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
        /// Exit
        /// </summary>
        public void Exit()
        {
            _done = true;
        }

        #endregion
    }
}
