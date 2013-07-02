using System;
using System.Collections;
using Agent.Contrib.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;

namespace MultiButtons
{
    public class Program
    {
        static Bitmap _display;
        static Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);

        /// <summary>
        /// A demonstration as to how to handle multiple button presses at the same time
        /// </summary>

        public static void Main()
        {

            ButtonHelper.ButtonSetup = new Buttons[]{ Buttons.TopRight, Buttons.MiddleRight, Buttons.BottomRight,};
            ButtonHelper.Current.OnButtonPress += Current_OnButtonPress;

            buttonStates = new Hashtable();
            buttonStates.Add(Buttons.TopRight, ButtonDirection.Up);
            buttonStates.Add(Buttons.MiddleRight, ButtonDirection.Up);
            buttonStates.Add(Buttons.BottomRight, ButtonDirection.Up);

            // initialize display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            // sample "hello world" code
            _display.Clear();
            _display.DrawText("Hello world.", fontNinaB, Color.White, 10, 64);
            _display.Flush();

            // go to sleep; all further code should be timer-driven or event-driven
            Thread.Sleep(Timeout.Infinite);
        }
        static Hashtable buttonStates = new Hashtable();
        static void Current_OnButtonPress(Buttons button, InterruptPort port, ButtonDirection direction, DateTime time)
        {
            buttonStates[button] = direction;

            if ((ButtonDirection) buttonStates[Buttons.TopRight] == ButtonDirection.Down && (ButtonDirection) buttonStates[Buttons.BottomRight] == ButtonDirection.Down)
            {
                //both top right and bottom right buttons are pressed down
                _display.Clear();
                _display.DrawText("YOU DID IT!", fontNinaB, Color.White, 10, 64);
                _display.Flush();
            }
            else
            {
                _display.Clear();
                _display.DrawText("Hello world.", fontNinaB, Color.White, 10, 64);
                _display.Flush();
                
            }
        }

    }
}
