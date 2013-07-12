using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Hardware.Viberate
{

    public class ViberateProvider
    {

        private static object _lock = new object();
        private static IViberate _vib;
        public static IViberate Current
        {
            get
            {
                lock (_lock)
                {
                    _vib = new DebugViberate();
                }
                return _vib;
            }
        }
    }
}
