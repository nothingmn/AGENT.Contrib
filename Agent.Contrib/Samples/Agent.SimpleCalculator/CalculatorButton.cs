using System;
using Agent.Contrib.Drawing;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace SimpleCalculator
{
    public class CalculatorButton
    {
        public string Text { get; set; }
        public Point Point { get; set; }
        public bool Selected { get; set; }

        public void Render(Bitmap screen, Font font, int height, int width, int borderBuffer, Color ForegroundColor, Color BackgroundColor)
        {
            if (Text != null && Text != "")
            {
                Color forColor = ForegroundColor;
                Color bColor = BackgroundColor;

                if (Selected)
                {
                    forColor = BackgroundColor;
                    bColor = ForegroundColor;
                }         
                screen.DrawRectangle(forColor, 1, Point.X, Point.Y, width + borderBuffer * 2,
                                     height + borderBuffer * 2, 0, 0, bColor, 0, 0,
                                     bColor, 0, 0, 255);

                screen.DrawText(Text, font, forColor, Point.X + borderBuffer, Point.Y + borderBuffer);
            }
        }
    }
}