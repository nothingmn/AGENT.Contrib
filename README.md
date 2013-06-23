Agent.Contrib
=============

A set of useful class libraries which should help folks create amazing Faces and Apps for AGENT SmartWatch.


I'm taking pull requests, and ideas for contributions.

If you browse the source real quick I have added a few Samples as to how to start using some of the features:

* SimpleWatchFace - just a super simple example as to how to print the time on the screen.
* SimpleWatchFaceWithButtons - simple time on the screen, and responds to the MiddleRight button to toggle showing the date on the screen.

Other highlights:
================

Drawing class:
* A bunch of useful methods for drawing watch hands (for analog watch faces)
* A method to draw a batter, with correct filler depending on the % of battery life
* Drawing text or images at the center of the screen
* Measuring Text width, based on fonts

Parse class:
* All sorts of string to number parsing methods, and converting byte[] to string

GeoLocation class
* Initial set of interfaces for defining location and position data.  If we all can agree on an interface, then it will just make it easier for the entire community to create apps and faces...


Hardware class
* ButtonHelper class is a threadsafe way for dealing with buttons.  Can just wire up an event for the buttons you care about.  No need to deal with InterruptPorts, and the class will automatically tell you which button and direction was clicked.


Reflection utilities
* More advanced class, used to load up other types dynamically, etc..



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


Using Multiple Buttons
======================

By default, watch faces only support the middle button the default for the button helper is to only enable the single button.  If you want to enable all of the buttons on the right side, just use this prior to any calls to the button helper:

For example, place it at the top of your Main() in Program.cs


            ButtonHelper.ButtonSetup = new Buttons[]
                {
                    Buttons.BottomRight, Buttons.MiddleRight, Buttons.TopRight
                };

This will override the default behaviour and enable all three of those buttons.

