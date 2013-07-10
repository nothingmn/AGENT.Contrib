using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware.Viberate
{
    public class DebugViberate : IViberate
    {
        public void Viberate(int duration)
        {
            Debug.Print("VIBERATE:" + duration.ToString());
        }

        public void Viberate(string pattern)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                Viberate(i);
            }
        }
    }
}
