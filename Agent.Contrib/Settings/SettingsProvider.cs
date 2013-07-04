using System;
using System.Collections;
using Microsoft.SPOT;

namespace Agent.Contrib.Settings
{
    public class SettingsProvider
    {

        private static Settings.ISettings settings;
        private static object _settingsLock = new object();
        public static ISettings Current
        {
            get
            {
                lock (_settingsLock)
                {
                    settings = new SimpleSettings();
                    settings.ApplicationSettings = new Hashtable();
                    return settings;
                }
            }
        }
    }
}
