using System;
using AGENT.Contrib.Communication.Channels;
using AGENT.Contrib.Drawing;
using AGENT.Contrib.Hardware;
using AGENT.Contrib.UI;
using AGENT.Contrib.Util;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;

namespace Countdown.Viber.App
{
    public class Program
    {
        private static Bitmap _display;
        private static Font menuFont = Resources.GetFont(Resources.FontResources.NinaB);
        private static CountdownTimer countdown;
        private static Menu menu;
        private static Drawing _drawing;

        private static void ResetAll()
        {
            menu.OnMenuItemClicked += menu_OnMenuItemClicked;
            menu.AutoRenderOnButtonPress = true;
            cdt.Change(Timeout.Infinite, Timeout.Infinite);
            AGENT.Contrib.Hardware.Viberate.ViberateProvider.Current.Viberate(5);
            ShowMenu();

        }

        public static void Main()
        {

            MultiButtonHelper mbh = new MultiButtonHelper();
            mbh.AddButtonHandler(new Buttons[] {Buttons.TopRight, Buttons.BottomRight,},
                                 new MultiButtonHelper.HandleClicks(ResetAll));

            // initialize display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);
            _drawing = new Drawing(_display);

            menu = new Menu(menuFont, _display);
            menu.OnMenuItemClicked += menu_OnMenuItemClicked;
            menu.Items.Add(new MenuItem() {Title = "1 Minute", CommandArg = "1"});
            menu.Items.Add(new MenuItem() {Title = "2 Minutes", CommandArg = "2"});
            menu.Items.Add(new MenuItem() {Title = "3 Minutes", CommandArg = "3"});
            menu.Items.Add(new MenuItem() {Title = "4 Minutes", CommandArg = "4"});
            menu.Items.Add(new MenuItem() {Title = "5 Minutes", CommandArg = "5"});
            menu.Items.Add(new MenuItem() {Title = "10 Minutes", CommandArg = "10"});
            menu.Items.Add(new MenuItem() {Title = "15 Minutes", CommandArg = "15"});
            menu.Items.Add(new MenuItem() {Title = "20 Minutes", CommandArg = "20"});
            menu.Items.Add(new MenuItem() {Title = "30 Minutes", CommandArg = "30"});
            menu.Items.Add(new MenuItem() {Title = "45 Minutes", CommandArg = "45"});
            menu.Items.Add(new MenuItem() {Title = "60 Minutes", CommandArg = "60"});

            ShowMenu();
            // go to sleep; all further code should be timer-driven or event-driven
            Thread.Sleep(Timeout.Infinite);
        }

        private static void ShowMenu()
        {
            Debug.Print("Rendering menu");
            menu.Render();

        }

        private static string FormatTimeIncrement(int increment)
        {
            string h = increment.ToString();
            if (h[0] == '-') h = (increment * -1).ToString();
            if (h.Length == 1) h = "0" + h;
            return h;
        }
        private static Timer cdt = new Timer(new TimerCallback(ShowCountDown), null, Timeout.Infinite, Timeout.Infinite);

        private static void ShowCountDown(object state)
        {
            _display.Clear();
            var remain = countdown.ReaminingDuration;

            var text = FormatTimeIncrement(remain.Hours) + ":" + FormatTimeIncrement(remain.Minutes) + ":" + FormatTimeIncrement(remain.Seconds);

            _drawing.DrawAlignedText(_display, Color.White, menuFont, text, HAlign.Center, 0, VAlign.Middle, 0);
            _display.Flush();
        }

        private static void menu_OnMenuItemClicked(Menu menu, MenuItem menuItem, DateTime time)
        {
            int duration = 0;

            if (Parse.TryParseInt(menuItem.CommandArg, out duration))
            {
                menu.OnMenuItemClicked -= menu_OnMenuItemClicked;
                menu.AutoRenderOnButtonPress = false;
                TimeSpan ts = new TimeSpan(0, duration, 0);
                countdown = new CountdownTimer(ts);
                countdown.OnCountdownTimerElapsed += countdown_OnCountdownTimerElapsed;
                countdown.Start();
                cdt.Change(0, 1000);
                ShowCountDown(null);
            }
        }

        private static void countdown_OnCountdownTimerElapsed(CountdownTimer timer, DateTime Timestamp)
        {
            ResetAll();
        }

    }
}