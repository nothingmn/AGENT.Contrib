using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Agent.Contrib.Drawing
{
    public class Drawing
    {
        private static object _screenLock = new object();
        private static Bitmap _screen;

        /// <summary>
        /// Lazy loaded default bitmap set tot he maxwidth and height once called.
        /// A static instance will be held for the lifetime of the app, unless you destory it.
        /// </summary>
        public static Bitmap Screen
        {
            get
            {
                lock (_screenLock)
                {
                    if (_screen == null) _screen = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);
                }
                return _screen;
            }
        }


        /// <summary>
        /// Measure the width that a set of text will take for a given font
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public int MeasureString(string text, Font font)
        {
            if (text == null || text.Trim() == "") return 0;
            int size = 0;
            for (int i = 0; i < text.Length; i++)
            {
                size += font.CharWidth(text[i]);
            }
            return size;
        }

        /// <summary>
        /// Find the center of the screen, for the given text and font
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public Point FindCenter(string text, Font font)
        {
            var size = MeasureString(text, font);
            var center = AGENT.Center;
            var centerText = size/2 - 2;
            var X = center.X - centerText;
            var Y = center.Y - (font.Height/2);
            return new Point(X, Y);
        }

        /// <summary>
        /// Draw text right in the center of the screen
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        public void DrawTextCentered(Bitmap screen, string text, Font font, Color color)
        {
            var center = FindCenter(text, font);
            screen.DrawText(text, font, color, center.X, center.Y);
        }

        /// <summary>
        /// Draw an image centered
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="imageData"></param>
        /// <param name="imageType"></param>
        public void DrawImageCentered(Bitmap screen, byte[] imageData, Bitmap.BitmapImageType imageType)
        {
            var img = new Bitmap(imageData, imageType);
            var p = new Point(AGENT.Center.X - (img.Width/2), AGENT.Center.Y - (img.Height/2));
            DrawImageAtPoint(screen, imageData, imageType, p);
        }

        /// <summary>
        /// Draw an image at a specific points
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="imageData"></param>
        /// <param name="imageType"></param>
        /// <param name="point"></param>
        public void DrawImageAtPoint(Bitmap screen, byte[] imageData, Bitmap.BitmapImageType imageType, Point point)
        {
            var img = new Bitmap(imageData, imageType);
            screen.DrawImage(point.X, point.Y, img, 0, 0, img.Height, img.Width);

        }


        private const int TRANSLATE_RADIUS_SECONDS = 47;
        private const int TRANSLATE_RADIUS_MINUTES = 40;
        private const int TRANSLATE_RADIUS_HOURS = 30;

        /// <summary>
        /// Determine where the outer point for the minute hand would be
        /// </summary>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public Point MinuteHandLocation(int minute, int second)
        {
            int min = (int) ((6*minute) + (0.1*second)); // Jump to Minute and add offset for 6 degrees over 60 seconds'
            return PointOnCircle(TRANSLATE_RADIUS_MINUTES, min + (-90), AGENT.Center);
        }


        /// <summary>
        /// Determin the hour hand point
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public Point HourHandLocation(int hour, int minute)
        {
            int hr = (int) ((30*(hour%12)) + (0.5*minute));
            // Jump to Hour and add offset for 30 degrees over 60 minutes
            return PointOnCircle(TRANSLATE_RADIUS_HOURS, hr + (-90), AGENT.Center);
        }

        /// <summary>
        /// Determine determine the seconds hand point location
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public Point SecondHandLocation(int second)
        {
            int sec = 6*second;
            return PointOnCircle(TRANSLATE_RADIUS_SECONDS, sec + (-90), AGENT.Center);
        }

        /// <summary>
        /// Paint the minute hand, with a given thickness
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public void PaintMinuteHand(Bitmap screen, Color color, int thickness, int minute, int second)
        {
            PaintLine(screen, color, thickness, AGENT.Center, MinuteHandLocation(minute, second));
        }

        /// <summary>
        /// Paint the seconds hand, with a given thickness
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="second"></param>
        public void PaintSecondHand(Bitmap screen, Color color, int thickness, int second)
        {

            PaintLine(screen, color, thickness, AGENT.Center, SecondHandLocation(second));
        }

        /// <summary>
        /// Paint the hour hand, with a given thickness
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        public void PaintHourHand(Bitmap screen, Color color, int thickness, int hour, int minute)
        {
            PaintLine(screen, color, thickness, AGENT.Center, HourHandLocation(hour, minute));

        }

        /// <summary>
        /// Paint a line from two points
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void PaintLine(Bitmap screen, Color color, int thickness, Point start, Point end)
        {
            screen.DrawLine(color, thickness, start.X, start.Y, end.X, end.Y);
        }


        private Point PointOnCircle(float radius, float angleInDegrees, Point origin)
        {
            return new Point(
                (int) (radius*System.Math.Cos(angleInDegrees*System.Math.PI/180F)) + origin.X,
                (int) (radius*System.Math.Sin(angleInDegrees*System.Math.PI/180F)) + origin.Y
                );
        }

        /// <summary>
        /// Paint all hands, a 0 width will not force the hand to not print.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="foreColor"></param>
        /// <param name="hourWidth"></param>
        /// <param name="minuteWidth"></param>
        /// <param name="secondWidth"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public void PaintHands(Bitmap screen, Color foreColor, int hourWidth, int minuteWidth, int secondWidth, int hour,
                               int minute, int second)
        {
            if (hourWidth > 0) PaintHourHand(screen, foreColor, hourWidth, hour, minute);
            if (minuteWidth > 0) PaintMinuteHand(screen, foreColor, minuteWidth, minute, second);
            if (secondWidth > 0) PaintSecondHand(screen, foreColor, secondWidth, second);

            screen.DrawEllipse(foreColor, 1, AGENT.Center.X, AGENT.Center.Y, 3, 3, Color.White, 0, 0, Color.White, 0, 0,
                               255);
            screen.DrawEllipse(foreColor, 1, AGENT.Center.X, AGENT.Center.Y, 2, 2, Color.Black, 0, 0, Color.White, 0, 0,
                               255);

        }

        /// <summary>
        /// Draw a simple battery to the screen
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="batteryPosition"></param>
        /// <param name="batteryWidth"></param>
        /// <param name="batteryHeight"></param>
        /// <param name="borderThickness"></param>
        /// <param name="batteryColor"></param>
        /// <param name="backColor"></param>
        public Size DrawBattery(Bitmap screen, Point batteryPosition, int batteryWidth, int batteryHeight,
                                int borderThickness, Color batteryColor, Color backColor, bool charging, int batteryLevel)
        {
            
            //calculate filler
            double fillerWidth = (batteryWidth - (borderThickness*2));
            double percent = ((double)batteryLevel * 0.01);
            double actualFillerWidth = fillerWidth*percent;

            //calculate nub
            double nubHight = batteryHeight*0.5;
            double nubWidth = batteryHeight*0.1;
            double nubTop = (((double) batteryHeight)/2) - (nubHight/2);
            Point nubPosition = new Point(batteryPosition.X + batteryWidth,
                                          batteryPosition.Y + (int) System.Math.Floor(nubTop));

            //draw main battery outline
            screen.DrawRectangle(batteryColor, borderThickness, batteryPosition.X, batteryPosition.Y, batteryWidth,
                                 batteryHeight, 0, 0, backColor, 0, 0, backColor, 0, 0, 255);
            //draw filler
            screen.DrawRectangle(backColor, 1, batteryPosition.X + 1, batteryPosition.Y + 1, (int) actualFillerWidth - 1,
                                 batteryHeight - 2, 0, 0, batteryColor, 0, 0, batteryColor, 0, 0, 255);
            //draw battery nub
            screen.DrawRectangle(batteryColor, 1, nubPosition.X, nubPosition.Y, (int) nubWidth, (int) nubHight, 0, 0,
                                 batteryColor, 0, 0, batteryColor, 0, 0, 255);
            //draw inner border
            var batterySize = new Size(batteryWidth + (int) nubWidth, batteryHeight);
            if (charging)
            {
                //draw charging indicator
                double iconHeight = (int)(nubHight*0.20);
                double iconTop = batteryPosition.Y + ((double)batteryHeight/2 - iconHeight/2)+1;
                double iconLeft = batteryPosition.X + 4;
                double iconWidth = (int)(iconLeft + actualFillerWidth - 5);

                screen.DrawLine(backColor, (int)iconHeight, (int)iconLeft, (int)iconTop, (int)iconWidth, (int)iconTop);


            }
            return batterySize;
        }

    }
}