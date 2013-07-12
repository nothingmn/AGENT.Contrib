using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Agent.Contrib.Hardware
{
    public class MultiButtonHelper
    {
        public delegate void HandleClicks();
        public MultiButtonHelper()
        {
            ButtonHelper.ButtonSetup = new Buttons[] {Buttons.TopRight, Buttons.MiddleRight, Buttons.BottomRight};
            ButtonHelper.Current.OnButtonPress += Current_OnButtonPress;
            clickTimer = new Timer(clickTimer_callback, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void AddButtonHandler(Buttons[] buttons, HandleClicks callback)
        {
            _buttonResponses.Add(buttons, callback);
        }

        private Timer clickTimer;
        void clickTimer_callback(object state)
        {
            DetectClicks();
            _buttonHitList.Clear();
        }
        string ListOfButtonsToString(Buttons[] listOfButtons)
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

        private void DetectClicks()
        {
            var hit = ListOfButtonsToString((Buttons[])_buttonHitList.ToArray(typeof(Buttons)));
            foreach (var b in _buttonResponses.Keys)
            {
                var bHit = ListOfButtonsToString((Buttons[])b);
                if (hit == bHit)
                {
                    _buttonHitList.Clear();
                    var method = (_buttonResponses[b] as HandleClicks);
                    if (method != null) method();
                    break;
                }
            }
        }

        private Hashtable _buttonResponses = new Hashtable();
        private ArrayList _buttonHitList = new ArrayList();
        public int MaxClickWaitTime = 250; //how fast can you click the button xNumber of times for it to count?
        void Current_OnButtonPress(Buttons button, InterruptPort port, ButtonDirection direction, DateTime time)
        {
            if (direction == ButtonDirection.Up)
            {
                _buttonHitList.Add(button);
                clickTimer.Change(MaxClickWaitTime, Timeout.Infinite);
            }
        }


    }
}
