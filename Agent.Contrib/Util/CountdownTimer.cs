using System;
using System.Threading;
using Microsoft.SPOT;

namespace Agent.Contrib.Util
{
    public delegate void CountdownTimerElapsed(CountdownTimer timer, DateTime Timestamp);

    /// <summary>
    /// Count down from X number of seconds
    /// a single event will be triggerd at expiry
    /// </summary>
    public class CountdownTimer
    {
        public event CountdownTimerElapsed OnCountdownTimerElapsed;
        private TimeSpan _StartDuration;
        public TimeSpan StartDuration { get { return _StartDuration; } }

        public TimeSpan ReaminingDuration
        {
            get { return TimeSpan.FromTicks(DateTime.Now.Ticks - target.Ticks); }
        }
        public bool IsRunning { get { return _isRunning; } }
        private long startTickCount;
        private DateTime target;
        private Timer countTimer;
        private bool _isRunning = false;
        public CountdownTimer(TimeSpan duration)
        {
            _StartDuration = duration;
            startTickCount = Microsoft.SPOT.Hardware.Utility.GetMachineTime().Ticks;
            target = DateTime.Now.AddTicks(duration.Ticks);
        }
        public void Start()
        {
            var x = _StartDuration.Ticks / TimeSpan.TicksPerMillisecond;
            countTimer = new Timer(new TimerCallback(Fire), null, (int)x, Timeout.Infinite);
            _isRunning = true;
        }

        private void Fire(object state)
        {
            _isRunning = false;
            countTimer.Change(Timeout.Infinite, Timeout.Infinite);
            if (OnCountdownTimerElapsed != null) OnCountdownTimerElapsed(this, DateTime.Now);
        }
    }
}