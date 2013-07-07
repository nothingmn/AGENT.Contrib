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
            Point[] sq3 = { new Point(20, 20), new Point(80, 90), new Point(100, 100), new Point(100, 60) };
            Point[] sq4 = {     new Point(30, 30), 
                                new Point(30, 70), 
                                new Point(100, 70), 
                                new Point(100, 30), 
                                new Point(80, 20), 
                                new Point(70, 30), 
                                new Point(70, 45),
                                new Point(50, 45),
                                new Point(50, 30)
                          };

            Drawing.DrawPoly(screen, sq, Color.White, 2, Color.White, Drawing.PolyFill.POLYFILL_SOLID);
            Drawing.DrawPoly(screen, sq3, Color.White, 2, Color.White, Drawing.PolyFill.POLYFILL_EMPTY);
            Drawing.DrawPoly(screen, sq2, Color.White, 1, Color.White, Drawing.PolyFill.POLYFILL_CROSS_RIGHT);
            Drawing.DrawPoly(screen, sq4, Color.Black, 2, Color.White, Drawing.PolyFill.POLYFILL_DOTS);
            

        }
    }
}
