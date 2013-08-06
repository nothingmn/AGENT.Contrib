using System;
using Microsoft.SPOT;

namespace AGENT.Contrib.Hardware.Viberate
{
    public interface IViberate
    {
        void Viberate(int duration);
        void Viberate(string pattern);
    }
}
