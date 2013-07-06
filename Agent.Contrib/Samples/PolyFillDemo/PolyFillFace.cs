using System;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Agent.Contrib.Notifications;
using Agent.Contrib.Settings;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace PolyFillDemo
{
    class PolyFillFace : FaceWithTrayBase, IFace
    {

        public PolyFillFace(IProvideNotifications notificationProvider, ISettings settings)
            : base(notificationProvider, settings)
        {

        }

        public override void Render(Bitmap screen)
        {
            if (base._screen == null) _screen = screen;
            
            // simple bitmap
            Point[] sq = { new Point(10, 10), new Point(10, 40), new Point(40, 40), new Point(40, 10)};
            Point[] sq2 = { new Point(60, 60), new Point(60, 100), new Point(100, 100), new Point(100, 60) };

            Drawing.DrawPoly(screen, sq, Color.White, 1, Color.White, Drawing.PolyFill.POLYFILL_SOLID);
            Drawing.DrawPoly(screen, sq2, Color.White, 1, Color.White, Drawing.PolyFill.POLYFILL_EMPTY);

        }
    }
}
