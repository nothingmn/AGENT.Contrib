using System;
using System.Threading;
using Microsoft.SPOT;

namespace Agent.Contrib.Face
{
    public class WatchFace
    {
        public IFace Face { get; set; }
        public Bitmap Screen { get; set; }
        public WatchFace(IFace face)
        {
            Face = face;
        }
        public void Start()
        {
            if (Face == null) throw new ArgumentNullException("Face cannot be null");

            // Included font is used in the clock

            var timer = new Timer(state =>
            {

                
                //clear the display
                Screen.Clear();

    
                //call the user code
                Face.Render(Screen);


                //flush the image out to the device
                Screen.Flush();


            }, null, 1, Face.UpdateSpeed);
            
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
