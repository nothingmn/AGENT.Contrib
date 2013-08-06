 
/
|---package.json
|---logo.png
|---render_logo.gif
|---v1
|----|--face.exe
|----|--AGENT.Contrib.dll
|---v2
|----|--face.exe
|----|--AGENT.Contrib.dll
 
 
etc..
 
package.json is json formatted document with some/all of these properties:
 
id : a unique id of the package (example: "SimpleAnalogWatch")
type : the type of of app this represents   (face, app, whatever)
version : the version of this app/face (1.1.1)
title : the display name of this app/face ("Simple Analog Watch Face")
author : who authored this package
owner : who or what entity owns this packages
url : more information about this package
description : long description of what the package does
icon : a string pointer to the file in the package which is the logo (logo.gif)
render_icon: a strng pointer to the file in the package which is the logo, ready for rendering on the AGENT device
language : the ISO639x language code (en-US)
tags : an arbitrary set of comma seperated values for meta tags
 
 
 
logo.gif is the art which is safe for rendering on the web, store, etc..
render_logo.gif is the art which is safe for rendering on the device
 
 
v1, v2 are versioned application hives, specific to the version of the AGENT OS you are running.  Each each major version of AGENT OS is incremented we need a way to keep the API's in sync with different versions of our application; thus the need to segregate and version our apps along side the AGENT OS.
 
Keep in mind that in a semantic version system, the MAJOR value is incremented when there are API breaking changes, which will impact our apps.  This will give the store/watch/smartphone apps to be able to understand versioning and match deployable apps on to a variety of devices over time...  http://semver.org/
 
This means a single package of my app can be installed on any version of the AGENT.  It would be up to the store and/or the app on the device (phone or watch) to be AGENT OS version aware and deploy the correct version.
 
All of the files under the vX folders represent the app, for that version of AGENT OS, and would be deployed with it.
 
 
If we can "standardize" early on this sort of packaging then it would be very easy for us to create msbuild tasks around this standard for the next release of the SDK; which will mean that when you use a AGENT Template in VS.NET, the output will be a deployable package; lifting the burden from the developer.  Not to mention tooling around this format.