using System.Collections;
using Agent.Contrib.Settings;
using Microsoft.SPOT;

namespace Agent.Contrib.Face
{
    public interface IFace
    {
        ISettings Settings { get; set; }
        void Render(Bitmap screen);
        int UpdateSpeed { get; }
    }
}
