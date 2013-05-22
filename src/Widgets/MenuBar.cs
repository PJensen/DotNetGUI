namespace DotNetGUI.Widgets
{
    /// <summary>
    /// MenuBar
    /// </summary>
    public class MenuBar : Widget
    {
        /// <summary>
        /// MenuBar
        /// </summary>
        public MenuBar(Window parent, params Menu.MenuItem[] items)
            : base("", parent.Location.X + 2, parent.Location.Y, 0, 0)
        {
            int x = 1;

            foreach (var m in items)
            {
                var tmpMenuButton = new Button(parent, m.Name, parent.Location.X + x, parent.Location.Y)
                {
                    Visible = true,
                };

                Menu.MenuItem m1 = m;

                tmpMenuButton.OkayEvent += (sender, e) => m1.MAction();

                Widgets.Add(tmpMenuButton);

                x += m.Name.Length + 3;
            }
        }

        /// <summary>
        /// Show
        /// </summary>
        public override void Show()
        {
            base.Show();

            Console.ResetCursorPosition();
        }
    }
}
