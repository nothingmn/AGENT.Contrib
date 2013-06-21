using System.Collections;
using Microsoft.SPOT;

namespace Agent.Contrib.Face
{
    public interface IFace
    {
        void Render(Bitmap screen);
        int UpdateSpeed { get; }
    }
}
