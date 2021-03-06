﻿using System.Diagnostics;
using DotNetGUI.Attributes;
using DotNetGUI.Events;
using DotNetGUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetGUI
{
    /// <summary>
    /// Widget
    /// </summary>
    public abstract class Widget : IDisposable, IColorScheme, IHasLocation
    {
        /// <summary>
        /// Retains the creation order of all widgets. If this gets cumbersome there should be a factory.
        /// </summary>
        internal static int CreationOrder = 0;

        /// <summary>
        /// _tabIndex
        /// </summary>
        int _addOrder = 0;

        /// <summary>
        /// selector
        /// </summary>
        int _selector;

        /// <summary>
        /// is this widget visible
        /// </summary>
        bool _visible;

        /// <summary>
        /// Focused
        /// </summary>
        static Widget _focus;

        /// <summary>
        /// the selection direction; up or down (true or false)
        /// </summary>
        private bool _selectionDirection;

        /// <summary>
        /// Create a new Widget
        /// </summary>
        /// <param name="text">the text</param>
        /// <param name="location">the location</param>
        /// <param name="size">the size</param>
        /// <param name="parent"> </param>
        protected Widget(string text, IPoint location, IDimensional size, Widget parent = null)
            : this(text, location.X, location.Y, size.Width, size.Height, parent)
        {

        }

        /// <summary>
        /// Create a new Widget
        /// </summary>
        /// <param name="text">the text for the widget</param>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="width">the widget of the widget</param>
        /// <param name="height">the height of the widget</param>
        /// <param name="parent"> </param>
        protected Widget(string text, int x, int y, int width, int height, Widget parent = null)
        {
            WidgetId = CreationOrder++;

            Size = new Size(width, height);
            Location = new Point(x, y);
            Text = text;
            Widgets = new List<Widget>();
            Console = new DisplayBuffer(width, height);

            // Most widgets are not selectable by default
            IsSelectable = false;
            Visible = true;

            // The parent widget
            Parent = parent;

            // TODO: Keyboard events only fired from root widget
            // this needs to be moved.
            if (WidgetId < 1)
            {
                GUI.Instance.KeyboardCallback += Instance_KeyboardCallback;
            }
        }


        /// <summary>
        /// Create a new Widget
        /// </summary>
        /// <param name="text">the text to initialize the widget with</param>
        /// <param name="region">the screen region for the widget</param>
        /// <param name="parent"> </param>
        protected Widget(string text, IScreenRegion region, Widget parent = null)
            : this(text, region.Location.X, region.Location.Y, region.Width, region.Height, parent)
        { }

        /// <summary>
        /// Create a new Widget
        /// </summary>
        /// <param name="text">the text to initialize the widget with</param>
        /// <param name="parent"> </param>
        protected Widget(string text, Widget parent = null)
            : this(text, 0, 0, 0, 0, parent)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        protected Widget(Widget parent = null)
            : this(string.Empty, 0, 0, 0, 0, parent)
        {
        }

        /// <summary>
        /// Create a new widget without text
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="width">the widget of the widget</param>
        /// <param name="height">the height of the widget</param>
        /// <param name="parent"></param>
        protected Widget(int x, int y, int width, int height, Widget parent = null)
            : this("", x, y, width, height, parent) { }

        /// <summary>
        /// Traverse widgets given a root widget
        /// - The widget location is temporarily shifted relative to the parent widget.
        /// - The predicate if passed is used otherwise skipped.
        /// </summary>
        /// <param name="action">the action to take for each</param>
        /// <param name="root">the root widget to start at</param>
        /// <param name="predicate">the predicate, will not take action if false.</param>
        internal static void Traverse(Action<Widget> action, Widget root, Predicate<Widget> predicate = null)
        {
            if (root.Widgets.Count <= 0)
                return;

            foreach (var w in root.Widgets)
            {
                if (predicate == null || predicate(w))
                {
                    lock (w)
                    {
                        if (w.Parent != null)
                        {
                            w.Location.X += w.Parent.Location.X;
                            w.Location.Y += w.Parent.Location.Y;
                        }

                        action(w);

                        if (w.Parent != null)
                        {
                            w.Location.X -= w.Parent.Location.X;
                            w.Location.Y -= w.Parent.Location.Y;
                        }
                    }
                }

                if (w.Widgets != null && w.Widgets.Count > 0)
                {
                    Traverse(action, w, predicate);
                }
            }
        }

        /// <summary>
        /// Instance_KeyboardCallback
        /// </summary>
        /// <param name="obj"></param>
        internal void Instance_KeyboardCallback(ConsoleKeyInfo obj)
        {
            // TODO: Determine if the visibility constraint should be here.
            // TODO: If removing the delegate on Hide !Visible is redundant.
            if (KeyboardEvent == null || !Visible)
                return;

            if (_focus != null && _focus.KeyboardEvent != null)
                _focus.KeyboardEvent(this, new GUIKeyboardEventArgs(obj));

            // Intercept all keyboard events to define top level behavior
            switch (obj.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.Tab:
                    lock (Widgets)
                    {
                        Widget[] tmpWidgets;

                        if (_selectionDirection)
                        {
                            tmpWidgets = Widgets.Where(w => w.Visible && w.IsSelectable)
                               .OrderByDescending(w => w.TabIndex)
                               .ToArray();
                        }
                        else
                        {
                            tmpWidgets = Widgets.Where(w => w.Visible && w.IsSelectable)
                                .OrderBy(w => w.TabIndex)
                                .ToArray();
                        }

                        if (tmpWidgets.Any())
                        {
                            if (_focus != null)
                            {
                                _focus.Selected = false;
                                _focus.Console.Invalidate();
                            }

                            _focus = tmpWidgets[Math.Abs(_selector) % tmpWidgets.Count()];
                            _focus.Selected = true;
                            _focus.Console.Invalidate();
                            _focus.OnWidgetSelectedEvent();

                            Debug.Assert(Widgets.Count(w => w.Selected) == 1,
                                "only one widget can be selected");
                        }

                        // shift selector up/down depending on modifier key
                        unchecked
                        {
                            _selector += ((obj.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift) ? -1 : 1;
                        }
                    }

                    break;
            }

            switch (obj.Key)
            {
                case ConsoleKey.UpArrow:
                    _selectionDirection = true;
                    break;
                case ConsoleKey.DownArrow:
                    _selectionDirection = false;
                    break;
            }
        }

        /// <summary>
        /// SeletablePredicate
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public static bool SeletablePredicate(Widget w)
        {
            return w.IsSelectable && w.Visible;
        }

        /// <summary>
        /// Console
        /// </summary>
        public DisplayBuffer Console { get; private set; }

        /// <summary>
        /// BG
        /// </summary>
        public ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// FG
        /// </summary>
        public ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        /// Refreshes the widget by re-writting the display buffer using the most recent settings.
        /// </summary>
        public void Refresh()
        {
            Console.BulkColorUpdate(this);
        }

        /// <summary>
        /// InitializeWidget
        /// </summary>
        public virtual void InitializeWidget()
        {
            WidgetProperties.Reflect(this);
            Console.ResetCursorPosition();
        }

        /// <summary>
        /// Show
        /// </summary>
        public virtual void Show()
        {
            Visible = true;

            Console.ResetCursorPosition();
        }

        /// <summary>
        /// Hide
        /// </summary>
        public virtual void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="widget">Add a widget</param>
        public void Add(Widget widget)
        {
            widget.TabIndex = _addOrder++;

            Widgets.Add(widget);
        }

        /// <summary>
        /// Add a collection of widgets as children to this widget
        /// </summary>
        /// <param name="widgets">the widgets to add to this widget</param>
        public void Add(IEnumerable<Widget> widgets)
        {
            foreach (var widget in widgets)
            {
                Add(widget);
            }
        }

        /// <summary>
        /// TabIndex
        /// </summary>
        public int TabIndex { get; private set; }

        /// <summary>
        /// Width
        /// </summary>
        public int Width
        {
            get { return Size.Width; }
        }

        /// <summary>
        /// Height
        /// </summary>
        public int Height
        {
            get { return Size.Height; }
        }

        /// <summary>
        /// Location
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public IDimensional Size { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Widgets
        /// </summary>
        public List<Widget> Widgets { get; set; }

        /// <summary>
        /// Visible
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (value == _visible)
                {
                    return;
                }

                _visible = value;

                OnVisibilityChangedEvent(new GUIVisibilityChangedEventArgs(value));
            }
        }

        /// <summary>
        /// WidgetID
        /// </summary>
        public int WidgetId { get; private set; }

        /// <summary>
        /// The parent widget
        /// </summary>
        public Widget Parent { get; private set; }

        #region Widget Selection

        /// <summary>
        /// IsSelectable
        /// </summary>
        public bool IsSelectable { get; private set; }

        /// <summary>
        /// Selected
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// OnWidgetSelectedEvent
        /// </summary>
        protected void OnWidgetSelectedEvent()
        {
            if (WidgetSelectedEvent != null)
            {
                WidgetSelectedEvent(this, new EventArgs());
            }
        }

        /// <summary>
        /// EnableSelection
        /// </summary>
        public void EnableSelection()
        {
            IsSelectable = true;
        }

        /// <summary>
        /// EnableSelection
        /// </summary>
        public void DisableSelection()
        {
            IsSelectable = false;
        }

        #endregion

        #region Events

        /// <summary>
        /// KeyboardEvent
        /// </summary>
        public event EventHandler<GUIKeyboardEventArgs> KeyboardEvent;

        /// <summary>
        /// WidgetSelectedEvent
        /// </summary>
        public event EventHandler WidgetSelectedEvent;

        /// <summary>
        /// VisibilityChangedEvent
        /// <remarks>occurs when visibility changes</remarks>
        /// </summary>
        public event EventHandler<GUIVisibilityChangedEventArgs> VisibilityChangedEvent;

        /// <summary>
        /// OnVisibilityChangedEvent
        /// </summary>
        /// <param name="e">visibility change event</param>
        protected virtual void OnVisibilityChangedEvent(GUIVisibilityChangedEventArgs e)
        {
            var handler = VisibilityChangedEvent;
            if (handler != null) handler(this, e);
        }

        #endregion

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Hide();
        }

        /// <summary>
        /// Box the widget
        /// </summary>
        public void Box()
        {
            Box(Text, 0, 0, Width, Height);
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            Console.Clear(this);
        }

        /// <summary>
        /// Box
        /// </summary>
        /// <param name="x">The X-Coordinate</param>
        /// <param name="y">The Y-Coordinate</param>
        /// <param name="w">The Width.</param>
        /// <param name="h">The Height.</param>
        public void Box(int x, int y, int w, int h)
        {
            if (w <= 2)
                throw new ArgumentException(
                    string.Format("Display box width must be greater than 2. Was {0}.", w));

            for (int c = 0; c < w; ++c)
            {
                Console.SetCursorPosition(x + c, y);
                if (c == 0)
                    Console.Write('╔');
                else if (c == w - 1)
                    Console.Write('╗');
                else
                    Console.Write('═');
            }
            for (int c = 1; c < h; ++c)
            {
                Console.SetCursorPosition(x, y + c);
                Console.Write('║');
                Console.SetCursorPosition(x + w - 1, y + c);
                Console.Write('║');
            }
            for (int c = 0; c < w; ++c)
            {
                Console.SetCursorPosition(x + c, y + h);
                if (c == 0)
                    Console.Write('╚');
                else if (c == w - 1)
                    Console.Write('╝');
                else
                    Console.Write('═');
            }

            var spacer = string.Empty;
            for (var c = 0; c < w - 2; ++c)
                spacer += " ";

            for (var c = 1; c < h; ++c)
            {
                Console.SetCursorPosition(x + 1, y + 1);
                Console.Write(spacer);
            }
        }

        /// <summary>
        /// Box
        /// </summary>
        /// <param name="aTitle">The Title</param>
        /// <param name="x">The X-Coordinate</param>
        /// <param name="y">The Y-Coordinate</param>
        /// <param name="w">The Width.</param>
        /// <param name="h">The Height.</param>
        public void Box(string aTitle, int x, int y, int w, int h)
        {
            while (true)
            {
                var l = aTitle.Length;
                if (l < w)
                {
                    Box(x, y, w, h);
                    Console.SetCursorPosition(x + ((w / 2) - (l / 2)), y);
                    Console.Write(aTitle);
                }
                else
                {
                    w = l + 2;
                    continue;
                }
                break;
            }
        }

        /// <summary>
        /// Box
        /// </summary>
        /// <param name="aTile">The Title</param>
        /// <param name="x">The X-Coordinate</param>
        /// <param name="y">The Y-Coordinate</param>
        /// <param name="w">The Width.</param>
        /// <param name="h">The Height.</param>
        /// <param name="ralign">Right Align?</param>
        public void Box(string aTile, int x, int y, int w, int h, bool ralign)
        {
            if (!ralign)
                Box(aTile, x, y, w, h);
            else Box(aTile, Console.Width - (((aTile.Length > w ? aTile.Length + 2 : w)) + x), y, w, h);
        }
    }
}
