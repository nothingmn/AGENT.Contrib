using System;
using System.Collections;
using System.Text;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Agent.Contrib.Hardware
{
    public class ButtonsSequenceDetector
    {
        private const int SEQUENCEDELAY = 1000;
       
        private StringBuilder _buttonSequence = new StringBuilder();
        private readonly Timer _sequenceTimer;
        private Object _lockTimer = new Object();

        private readonly ButtonHelper _buttonHelper;

        //Event for sequence detection
        public delegate void SequenceHandler(string sequence);
        public event SequenceHandler OnSequenceDetected;
        public event SequenceHandler OnUnattendedSequence;

        public ArrayList ButtonsToTrack
        {
            get;
            set;
        }

        //Table containing the sequences to detect and the associated handlers.
        private Hashtable _sequencesToTrack;

        public ButtonsSequenceDetector(ButtonHelper buttonHelper)
        {
            _buttonHelper = buttonHelper;           

            //Link to the OnButtonPress event
            _buttonHelper.OnButtonPress += ButtonPressDetected;
            ButtonsToTrack = new ArrayList();
            //Create the timer but set it not to start
            _sequenceTimer = new Timer(new TimerCallback(this.CheckSequence), null, Timeout.Infinite, 0);

            _sequencesToTrack = new Hashtable();
        }

        private void ButtonPressDetected(Buttons button, InterruptPort port, ButtonDirection direction, DateTime time)
        {
            if (ButtonsToTrack.Contains(button))
            {
                 AddToSequence(button, direction);
            }           
        }

        private void AddToSequence(Buttons button, ButtonDirection direction)
        {
            lock (_lockTimer)
            {
                _buttonSequence.Append(button.ToString());
                _buttonSequence.Append(direction.ToString());                

                // Start or Reset the timer
                _sequenceTimer.Change(SEQUENCEDELAY, 0);
            }
            
        }

        private void CheckSequence(object state)
        {
            String detectedSequence = "";

            lock (_lockTimer)
            {
                detectedSequence = _buttonSequence.ToString();

                //Clean the current sequence and disable the timer
                _buttonSequence.Clear();
                _sequenceTimer.Change(Timeout.Infinite, 0);
            }

            Debug.Print("SequenceIntervalExpired");
            if (OnSequenceDetected != null)
            {
                OnSequenceDetected(detectedSequence);
            }           

            if (_sequencesToTrack.Contains(detectedSequence))
            {
                var handler = (SequenceHandler)_sequencesToTrack[detectedSequence];
                handler(detectedSequence);
            }
            else
            {
                if (OnUnattendedSequence != null)
                {
                    OnUnattendedSequence(detectedSequence);
                }
            }
            
        }

        public void AddSequence(ButtonsSequence sequence)
        {
            _sequencesToTrack.Add(sequence.Sequence, sequence.Handler);
        }

        public void RemoveSequence(ButtonsSequence sequence)
        {
            _sequencesToTrack.Remove(sequence.Sequence);
        }
    }
}
