using System;
using AGENT.Contrib.Settings;
using Microsoft.SPOT;

namespace AGENT.Contrib.Face
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
