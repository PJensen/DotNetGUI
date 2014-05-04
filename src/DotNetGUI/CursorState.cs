using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DotNetGUI
{
    /// <summary>
    /// <see cref="CursorState"/>
    /// Cursor state objects trap the current cursor state
    /// at the precise time of creation. Additionally provides a 
    /// cursor state stack for poping and pushing from.
    /// </summary>
    public class CursorState : IEquatable<CursorState>
    {
        #region constructor

        /// <summary>
        /// private <see cref="CursorState"/> constructor since these objects
        /// should only be created and accessed via the stack.
        /// </summary>
        private CursorState()
        {
        }

        #endregion

        #region backing store

        /// <summary>
        /// CursorStates
        /// <remarks>a stack that contains cursor states</remarks>
        ///  </summary>
        private readonly static Stack<CursorState> CursorStateStack = new Stack<CursorState>();

        /// <summary>
        /// the cursor left at the time of this objects creation
        /// </summary>
        public readonly int CursorLeft = Console.CursorLeft;

        /// <summary>
        /// the cursor top at the time of this objects creation
        /// </summary>
        public readonly int CursorTop = Console.CursorTop;

        /// <summary>
        /// the foreground color at the time of this objects creation
        /// </summary>
        public readonly ConsoleColor ForegroundColor = Console.ForegroundColor;

        /// <summary>
        /// the background color at the time of this objects creation
        /// </summary>
        public readonly ConsoleColor BackgroundColor = Console.BackgroundColor;

        /// <summary>
        /// The cursor size
        /// </summary>
        public readonly int CursorSize = Console.CursorSize;

        /// <summary>
        /// The cursor visible status
        /// </summary>
        public readonly bool CursorVisible = Console.CursorVisible;

        #endregion

        /// <summary>
        /// Set the cursor state using this object
        /// <remarks>The "Set" method has side affects</remarks>
        /// </summary>
        public void Set()
        {
            Console.CursorVisible = CursorVisible;
            Console.CursorLeft = CursorLeft;
            Console.CursorTop = CursorTop;
            Console.CursorSize = CursorSize;
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
        }

        /// <summary>
        /// Pop the cursor state from the stack
        /// <remarks>The "Set" method does not have side affects</remarks>
        /// </summary>
        public static CursorState Pop(bool set = true)
        {
            if (!CursorStateStack.Any())
            {
                throw new DotNetGUIException();
            }

            return CursorStateStack.Pop();
        }

        /// <summary>
        /// Push the cursor state onto the stack
        /// </summary>
        public static void Push()
        {
            CursorStateStack.Push(new CursorState());
        }

        /// <summary>
        /// Returns the cursor state that is on the top of the stack
        /// <remarks>
        /// If there is nothing on the stack; simply returns the current cursor state.
        /// </remarks>
        /// </summary>
        /// <returns>The top-most <see cref="CursorState"/></returns>
        public static CursorState Peek()
        {
            if (!CursorStateStack.Any())
            {
                throw new DotNetGUIException();
            }

            return CursorStateStack.Peek();
        }

        #region equality members

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other">the other <see cref="CursorState"/></param>
        /// <returns>true if equal</returns>
        public bool Equals(CursorState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return CursorVisible.Equals(other.CursorVisible) &&
                CursorSize == other.CursorSize &&
                BackgroundColor == other.BackgroundColor &&
                ForegroundColor == other.ForegroundColor &&
                CursorTop == other.CursorTop &&
                CursorLeft == other.CursorLeft;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">the object to compare to</param>
        /// <returns>true if equal</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(CursorState) && Equals((CursorState)obj);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CursorVisible.GetHashCode();
                hashCode = (hashCode * 397) ^ CursorSize;
                hashCode = (hashCode * 397) ^ (int)BackgroundColor;
                hashCode = (hashCode * 397) ^ (int)ForegroundColor;
                hashCode = (hashCode * 397) ^ CursorTop;
                hashCode = (hashCode * 397) ^ CursorLeft;
                return hashCode;
            }
        }

        /// <summary>
        /// == operator
        /// </summary>
        /// <param name="left">the left hand side</param>
        /// <param name="right">the right hand side</param>
        /// <returns>true if equal</returns>
        public static bool operator ==(CursorState left, CursorState right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// != operator
        /// </summary>
        /// <param name="left">the left hand side</param>
        /// <param name="right">the right hand side</param>
        /// <returns>true if not equal</returns>
        public static bool operator !=(CursorState left, CursorState right)
        {
            return !Equals(left, right);
        }

        #endregion

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("CursorVisible: {0}, CursorSize: {1}, BackgroundColor: {2}, ForegroundColor: {3}, CursorTop: {4}, CursorLeft: {5}",
                CursorVisible, CursorSize, BackgroundColor, ForegroundColor, CursorTop, CursorLeft);
        }
    }
}
