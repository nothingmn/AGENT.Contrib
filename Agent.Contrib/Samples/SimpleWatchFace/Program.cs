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
