Agent.Contrib
=============

A set of useful class libraries which should help folks create amazing Faces and Apps for AGENT SmartWatch.


I'm taking pull requests, and ideas for contributions.

If you browse the source real quick I have added a few Samples as to how to start using some of the features:

* SimpleWatchFace - just a super simple example as to how to print the time on the screen.
* SimpleWatchFaceWithButtons - simple time on the screen, and responds to the MiddleRight button to toggle showing the date on the screen.

Other highlights:
================
Documentation:
* [Style Guide] (https://github.com/nothingmn/AGENT.Contrib/wiki/Style-Guide)
* [Development Guides] (https://github.com/nothingmn/AGENT.Contrib/wiki/Development-Guides)
* [Micro Tips for .NET Developers] (https://github.com/nothingmn/AGENT.Contrib/wiki/Micro-Tips-for-.NET-Developers)
* [Troubleshooting] (https://github.com/nothingmn/AGENT.Contrib/wiki/Troubleshooting)
* [Hardware Specs] (https://github.com/nothingmn/AGENT.Contrib/wiki/Hardware-Specs)
* [Smartphone Device Support] (https://github.com/nothingmn/AGENT.Contrib/wiki/Smartphone-Device-Support)
* [Internationalization & Localization] (https://github.com/nothingmn/AGENT.Contrib/wiki/Internationalization-&-Localization)
* [Building your own prototype] (https://github.com/nothingmn/AGENT.Contrib/wiki/Building-your-own-prototype)


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
* Create a new "Micro .NET Framework" "Command line" application 
* Import your favourite font as a resource, here we will use NinaB
* Add a reference to Microsoft.SPOT.Graphics
* Add a reference to AGENT.Contrib project
* Add a new file to the project, called "BasicFace.cs"
* Update BasicFace.cs to the following code:

```
using System.Globalization;
using Agent.Contrib.Face;
using Agent.Contrib.Settings;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace SimpleWatchFace
{
    public class SimpleFace : IFace
    {
        public ISettings Settings { get; set; }
        private Font font = Resources.GetFont(Resources.FontResources.NinaB);
        public void Render(Bitmap screen)
        {
            screen.DrawText(
                Settings.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortTimePattern),
                font,
                Color.White, 
                0,
                0
                );
        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}

```
* Update Program.cs to the following:

```
using Agent.Contrib.Face;

namespace SimpleWatchFace
{
    public class Program
    {
        public static void Main()
        {
            var watch = new WatchFace(new SimpleFace());
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

