using System;
using System.Threading;
using Agent.Contrib.Settings;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Agent.Contrib.Face
{
    public class WatchFace : IWatchFace
    {
        public ISettings Settings { get; set; }
        public IFace Face { get; set; }
        public Bitmap Screen { get; set; }

        /// <summary>
        /// Reference to the internal timer used to refresh the face
        /// </summary>
        private Timer _timer;
        /// <summary>
        /// Timer interval in ms
        /// </summary>
        private int _timerPeriod;

        public void Render()
        {

            //clear the display
            Screen.Clear();


            //call the user code
            Face.Render(Screen);


            //flush the image out to the device
            Screen.Flush();

            //Update the timer period if it has been changed
            if (_timerPeriod != Face.UpdateSpeed)
            {
                _timerPeriod = Face.UpdateSpeed;
                _timer.Change(1, _timerPeriod);
            }
        }
        public WatchFace(IFace face, Bitmap screen = null, ISettings settings = null)
        {            
            Face = face;
            if(screen == null) screen = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);
            if (settings == null) settings = SettingsProvider.Current;
            Screen = screen;
            Settings = settings;
            if (Face.Settings == null) Face.Settings = settings;
        }

        public void Start()
        {
            if (Face == null) throw new ArgumentNullException("Face cannot be null");

            // Included font is used in the clock

            //Start timer for screen refresh and keep a reference
            //so the refresh period can be update later.
            _timerPeriod = Face.UpdateSpeed;
            _timer = new Timer(state =>
            {

                Render();

            }, null, 1, _timerPeriod);
            
            Thread.Sleep(Timeout.Infinite);
        }


    }
}
