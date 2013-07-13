using System;
using System.Threading;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware.Viberate
{
    public class DebugViberate : IViberate
    {

        /// <summary>
        /// Viberate the motor for a number of milliseconds
        /// </summary>
        /// <param name="duration"></param>
        public void Viberate(int duration)
        {
            Debug.Print("VIBERATE:" + duration.ToString());
        }


        /// <summary>
        /// Viberate the motor for a given patterns of S and L's
        /// S is short, 500ms
        /// L is long, 1000ms (1 second)
        /// P will pause for 100ms
        /// </summary>
        /// <param name="pattern"></param>
        public void Viberate(string pattern)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                var signal = pattern[i];
                if (signal.ToLower() == 's') Viberate(500);
                if (signal.ToLower() == 'l') Viberate(1000);
                if (signal.ToLower() == 'p') Thread.Sleep(100);
            }
        }
    }
}
