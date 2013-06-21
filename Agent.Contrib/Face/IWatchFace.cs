using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Face
{
    public interface IWatchFace
    {
        IFace Face { get; set; }
        Bitmap Screen { get; set; }
        void Start();
    }
}
