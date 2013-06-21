Agent.Contrib
=============

A set of useful class libraries which should help folks create amazing Faces and Apps for AGENT SmartWatch.


Sample Usage
============
* Download and build the Agent.Contrib project.
* Create a new Micro .NET Framework Command line application 
* Import your favorite font as a resource, here we will use NinaB
* Add a reference to Microsoft.SPOT.Graphics
* Add a new file to the project, called "BasicFace.cs"
* Update BasicFace.cs to the following code:

```
using System;
using Agent.Contrib.Face;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace BasicWatchFace
{
    public class BasicFace : IFace
    {
        public void Render(Microsoft.SPOT.Bitmap screen)
        {
            Font nina = Resources.GetFont(Resources.FontResources.NinaB);
            screen.DrawText(DateTime.Now.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortTimePattern), nina, Color.White, 0, 0);
        }

        public int UpdateSpeed
        {
            get { return 1000*60; }
        }
    }
}


```
* Update Program.cs to the following:

```
using Agent.Contrib.Face;

namespace BasicWatchFace
{
    public class Program
    {
        public static void Main()
        {
            IFace face = new BasicFace();
            var watch = new WatchFace(face);
            watch.Start();
        }

    }
}

```

Build and run!


