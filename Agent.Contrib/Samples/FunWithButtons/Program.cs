using System;
using System.Collections;
using Agent.Contrib.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;

namespace FunWithButtons
{
    public class Program
    {
        static Bitmap _display;
        static Timer _updateClockTimer;

        public delegate void HandleClicks();

        public static int MaxClickWaitTime = 250; //how fast can you click the button xNumber of times for it to count?

        public static void Main()
        {
            //wire up three button clicks to the TripleClick Method
            _buttonResponses.Add(new Buttons[] { Buttons.MiddleRight, Buttons.MiddleRight, Buttons.MiddleRight }, new HandleClicks(TripleClick));
            _buttonResponses.Add(new Buttons[] { Buttons.MiddleRight, Buttons.MiddleRight }, new HandleClicks(DoubleClick));
            _buttonResponses.Add(new Buttons[] { Buttons.MiddleRight }, new HandleClicks(SingleClick));

            ButtonHelper.Current.OnButtonPress += Current_OnButtonPress;

            // initialize our display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            // display the time immediately
            UpdateTime(null);

            // obtain the current time
            DateTime currentTime = DateTime.Now;
            // set up timer to refresh time every minute
            TimeSpan dueTime = new TimeSpan(0, 0, 0, 59 - currentTime.Second, 1000 - currentTime.Millisecond); // start timer at beginning of next minute
            TimeSpan period = new TimeSpan(0, 0, 1, 0, 0); // update time every minute
            _updateClockTimer = new Timer(UpdateTime, null, dueTime, period); // start our update timer

            clickTimer = new Timer(clickTimer_callback, null, Timeout.Infinite, Timeout.Infinite);

            // go to sleep; time updates will happen automatically every minute
            Thread.Sleep(Timeout.Infinite);
        }

        private static Timer clickTimer;
        static void clickTimer_callback(object state)
        {
            DetectClicks();
            _buttonHitList.Clear();
            Debug.Print("CLEARED TICKS!");
        }
        private static void TripleClick()
        {
            Debug.Print("TRIPLE CLICK SUCCESS!");
        }
        private static void DoubleClick()
        {
            Debug.Print("DOUBLE CLICK SUCCESS!");
        }
        private static void SingleClick()
        {
            Debug.Print("SINGLE CLICK SUCCESS!");
        }
        private static Hashtable _buttonResponses = new Hashtable();
        private static ArrayList _buttonHitList = new ArrayList();
        static void Current_OnButtonPress(Buttons button, InterruptPort port, ButtonDirection direction, DateTime time)
        {
            if (direction == ButtonDirection.Up)
            {
                _buttonHitList.Add(button);
                clickTimer.Change(MaxClickWaitTime, Timeout.Infinite);
            }
        }
        static string ListOfButtonsToString(Buttons[] listOfButtons)
        {
            string sb = "";
            foreach (Buttons b in listOfButtons)
            {
                if (b == Buttons.BottomLeft) sb += "BL";
                if (b == Buttons.BottomRight) sb += "BR";
                if (b == Buttons.MiddleRight) sb += "MR";
                if (b == Buttons.TopLeft) sb += "TL";
                if (b == Buttons.TopRight) sb += "TR";
            }
            return sb;
        }

        private static void DetectClicks()
        {
            Debug.Print("DETECTING CLICKS");
            var hit = ListOfButtonsToString((Buttons[])_buttonHitList.ToArray(typeof(Buttons)));
            Debug.Print("_buttonHitList:--->" + hit);
            foreach (var b in _buttonResponses.Keys)
            {
                var bHit = ListOfButtonsToString((Buttons[])b);
                Debug.Print("bHit:" + bHit);
                if (hit == bHit)
                {
                    _buttonHitList.Clear();
                    var method = (_buttonResponses[b] as HandleClicks);
                    if (method != null) method();
                    break;
                }
            }
            Debug.Print("/DETECTING CLICKS");
        }

        static void UpdateTime(object state)
        {
            // obtain the current time
            DateTime currentTime = DateTime.Now;
            // clear our display buffer
            _display.Clear();

            // add your watchface drawing code here
            Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);
            _display.DrawText(currentTime.Hour.ToString("D2") + ":" + currentTime.Minute.ToString("D2"), fontNinaB, Color.White, 46, 58);

            // flush the display buffer to the display
            _display.Flush();
        }

    }
}
