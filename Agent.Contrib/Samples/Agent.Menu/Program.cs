using System;
using System.Threading;
using Agent.Contrib.Hardware;
using Agent.Contrib.UI;
using Microsoft.SPOT;

namespace Agent.MenuApp
{
    public class Program
    {
        public static void Main()
        {
            //make sure we are using all of the right buttons
            ButtonHelper.ButtonSetup = new Buttons[]{ Buttons.TopRight, Buttons.MiddleRight, Buttons.BottomRight };

            //get our menu font
            var font = Resources.GetFont(Resources.FontResources.NinaB);
            //get a sample image for the menu items
            var arrow = new Bitmap(Resources.GetBytes(Resources.BinaryResources.right_arrow), Bitmap.BitmapImageType.Gif);
            //new up our menu
            var menu = new Agent.Contrib.UI.Menu(font);

            //setup our menu items
            menu.Items.Add(new MenuItem() { Title = "Hello", CommandName = "Hello", CommandArg = "World", Image = arrow });
            menu.Items.Add(new MenuItem() { Title = "World", CommandName = "World", CommandArg = "World", Image = arrow });
            menu.Items.Add(new MenuItem() {Title = "This is not so long", CommandName = "NotLong", CommandArg = "World"});
            menu.Items.Add(new MenuItem() { Title = "This text is very very long", CommandName = "Long", CommandArg = "World" });
            menu.Items.Add(new MenuItem() { Title = "Nice and short", CommandName = "Short", CommandArg = "World" });
            menu.Items.Add(new MenuItem() { Title = "A", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "B", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "C", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "D", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "E", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "F", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "G", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "H", CommandName = "A", CommandArg = "A" });
            menu.Items.Add(new MenuItem() { Title = "I", CommandName = "A", CommandArg = "A" });

            //responid to item clicks
            menu.OnMenuItemClicked += menu_OnMenuItemClicked;

            //render our menu
            menu.Render();

            //done
            System.Threading.Thread.Sleep(Timeout.Infinite);
        }

        static void menu_OnMenuItemClicked(Agent.Contrib.UI.Menu menu, MenuItem menuItem, DateTime time)
        {
            Debug.Print(menuItem.Title + " was clicked at " + time.ToString());

        }

    }
}
