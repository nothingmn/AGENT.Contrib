using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware.Viberate
{
    public interface IViberate
    {
        void Viberate(int duration);
        void Viberate(string pattern);
    }
}
