using System;
using Agent.Contrib.Settings;
using Microsoft.SPOT;

namespace Agent.Contrib.Face
{
    public interface IWatchFace
    {
        ISettings Settings { get; set; }
        IFace Face { get; set; }
        Bitmap Screen { get; set; }
        void Start();
        void Render();
    }
}
