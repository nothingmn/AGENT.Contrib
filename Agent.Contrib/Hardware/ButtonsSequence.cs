using System;
using System.Text;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware
{
    public class ButtonsSequence
    {
        private StringBuilder _encodedSequence = new StringBuilder();
        public string Sequence { get { return _encodedSequence.ToString(); } }

        private ButtonsSequenceDetector.SequenceHandler _handler;
        public ButtonsSequenceDetector.SequenceHandler Handler { get { return _handler; } }

        public void AddStep(Buttons button, ButtonDirection direction)
        {
            _encodedSequence.Append(button.ToString());
            _encodedSequence.Append(direction.ToString());
        }

        public void AddHandler(ButtonsSequenceDetector.SequenceHandler handler)
        {
            _handler = handler;
        }

        public void Clear()
        {
            _encodedSequence.Clear();
        }
    }
}
