using System.Collections;
using AGENT.Contrib.Settings;
using Microsoft.SPOT;

namespace AGENT.Contrib.Face
{
    public interface IFace
    {
        ISettings Settings { get; set; }
        void Render(Bitmap screen);
        int UpdateSpeed { get; }
    }
}
