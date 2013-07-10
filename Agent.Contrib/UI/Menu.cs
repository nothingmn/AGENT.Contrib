using System;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation.Media;
using Agent.Contrib.Hardware;

namespace Agent.Contrib.UI
{
    public class Menu
    {
        public static int AgentSize = 128;

        public delegate void MenuItemClicked(Menu menu, MenuItem menuItem, DateTime time);

        public event MenuItemClicked OnMenuItemClicked;
        private Bitmap _screen = null;
        public int SelectedIndex { get; set; }
        public ArrayList Items { get; set; }
        private int _maxVisible = 0;

        public Font MenuFont { get; set; }

        public Menu(Font menuFont, Bitmap screen = null)
        {
            _screen = screen;
            if (_screen == null) _screen = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            MenuFont = menuFont;
            
            SelectedIndex = 0;
            Items = new System.Collections.ArrayList();

            ButtonHelper.Current.OnButtonPress += buttonHelper_OnButtonPress;

            _maxVisible = AgentSize/menuFont.Height;
        }

        void buttonHelper_OnButtonPress(Buttons button, InterruptPort port, ButtonDirection direction, DateTime time)
        {
            if (direction == ButtonDirection.Up)
            {
                if (button == Buttons.TopRight) SelectedIndex--;
                if (button == Buttons.BottomRight) SelectedIndex++;
                if (SelectedIndex < 0) SelectedIndex = 0;
                if (SelectedIndex >= Items.Count) SelectedIndex = Items.Count-1;
                
                if (button == Buttons.MiddleRight)
                {
                    MenuItem item = (Items[SelectedIndex] as MenuItem);
                    if (OnMenuItemClicked != null) OnMenuItemClicked(this, item, time);
                }
                Render();
            }
        }

        public void Render()
        {
            _screen.Clear();

            //border
            _screen.DrawRectangle(Color.White, 1, 0, 0, AgentSize, AgentSize, 0, 0, Color.Black, 0, 0, Color.Black, 0, 0,
                                  255);

            int first = 0;
            int last = Items.Count-1;
            if ((last - first) > _maxVisible)
            {
                first = SelectedIndex - (_maxVisible/2);
                if (first < 0)
                {
                    first = 0;
                }

                last = first +  _maxVisible - 1;
                if (last > Items.Count - 1) last = Items.Count - 1;

            }

            int top = 0;
            int height = MenuFont.Height;

            for (int index = first; index <= last; index++)
            {
                MenuItem item = (Items[index] as MenuItem);
                item.Selected = false;
                if (index == SelectedIndex) item.Selected = true;

                Color backColor = Color.White;
                Color textColor = Color.Black;
                if (item.Selected)
                {
                    backColor = Color.Black;
                    textColor = Color.White;
                }
                _screen.DrawRectangle(backColor, 1, 1, top, Menu.AgentSize - 2, height, 0, 0, backColor, 0, 0, backColor,
                                      0,
                                      0, 255);

                _screen.DrawText(item.Title, MenuFont, textColor, 16, top + 1);

                if (item.Image != null) _screen.DrawImage(1, top, item.Image, 0,0,item.Image.Width, item.Image.Height);
                top += height + 1;
            }
            _screen.Flush();
        }
        
 

    }
}