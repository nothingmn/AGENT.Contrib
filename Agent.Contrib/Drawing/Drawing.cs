using System;
using Agent.Contrib.Hardware;
using Agent.Contrib.Notifications;
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
            double fillerWidth = (batteryWidth - (borderThickness*1));
            double percent = ((double)batteryLevel * 0.01);
            double actualFillerWidth = fillerWidth*percent;

            //calculate nub
            double nubHight = batteryHeight*0.5;
            double nubWidth = batteryHeight*0.1;
            double nubTop = (((double) batteryHeight)/2) - (nubHight/2);
            Point nubPosition = new Point(batteryPosition.X + batteryWidth,
                                          batteryPosition.Y + (int) System.Math.Floor(nubTop));

           //draw filler
            int paintedFiller = (int) actualFillerWidth;
            if (paintedFiller < 0) paintedFiller = 0;
            screen.DrawRectangle(backColor, 1, batteryPosition.X + 1, batteryPosition.Y + 1, paintedFiller,
                                 batteryHeight - 2, 0, 0, batteryColor, 0, 0, batteryColor, 0, 0, 255);

            //draw main battery outline
            screen.DrawRectangle(batteryColor, borderThickness, batteryPosition.X, batteryPosition.Y, batteryWidth,
                                 batteryHeight, 0, 0, backColor, 0, 0, backColor, 0, 0, 0);
            
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
        public void PaintSkinnyHands(Bitmap screen, DateTime time, Point center)
        {
            PaintHourHand(screen, Color.White, 3, time.Hour, time.Minute);
            PaintMinuteHand(screen, Color.White, 2, time.Minute,
                                           time.Second);
            //PaintSecondHand(screen,Color.White, 1, time.CurrentTime.Second);

            screen.DrawEllipse(Color.White, 1, center.X, center.Y, 3, 3, Color.White, 0, 0,
                                  Color.White, 0, 0, 255);
            screen.DrawEllipse(Color.White, 1, center.X, center.Y, 2, 2, Color.Black, 0, 0,
                                              Color.White, 0, 0, 255);

        }

        private Bitmap _EmailImage = null;
        public Bitmap EmailImage
        {
            get
            {
                if (_EmailImage == null)
                    _EmailImage = new Bitmap(ContribResources.GetBytes(ContribResources.BinaryResources.Mail),
                                             Bitmap.BitmapImageType.Gif);
                return _EmailImage;
            }
        }
        private Bitmap _textImage = null;
        public Bitmap TextImage
        {
            get
            {
                if (_textImage == null)
                    _textImage = new Bitmap(ContribResources.GetBytes(ContribResources.BinaryResources.Envelope),
                                             Bitmap.BitmapImageType.Gif);
                return _textImage;
            }
        }
        private Bitmap _calendarImage = null;
        public Bitmap CalendarImage
        {
            get
            {
                if (_calendarImage == null)
                    _calendarImage = new Bitmap(ContribResources.GetBytes(ContribResources.BinaryResources.Time),
                                             Bitmap.BitmapImageType.Gif);
                return _calendarImage;
            }
        }
        private Bitmap _VoiceMailImage = null;
        public Bitmap VoiceMailImage
        {
            get
            {
                if (_VoiceMailImage == null)
                    _VoiceMailImage = new Bitmap(ContribResources.GetBytes(ContribResources.BinaryResources.VoiceMail),
                                             Bitmap.BitmapImageType.Gif);
                return _VoiceMailImage;
            }
        }

        public void DrawTray(Bitmap screen, IProvideNotifications notificationProvider, Font font )
        {
            //battery level
            var level = Battery.Level;// Util.Random.Next(0, 100);
            this.DrawBattery(screen, new Point(1, 0), 13, 9, 1, Color.White, Color.Black, Battery.Charging,
                                level);
            screen.DrawText(level.ToString(), font, Color.White, 15, -2);

            var notificationSummary = new NotificationSummary(notificationProvider);
            if (notificationSummary.CalendarCount > 99) notificationSummary.CalendarCount = 99;
            if (notificationSummary.EmailCount > 99) notificationSummary.EmailCount = 99;
            if (notificationSummary.TextCount > 99) notificationSummary.TextCount = 99;
            if (notificationSummary.VoiceCount > 99) notificationSummary.VoiceCount = 99;

            if (notificationSummary.EmailCount > 0)
            {
                Debug.Print("Emails: " + notificationSummary.EmailCount.ToString());
                screen.DrawImage(33, 0, EmailImage, 0, 0, EmailImage.Width, EmailImage.Height);
                screen.DrawText(notificationSummary.EmailCount.ToString(), font, Color.White, 45, -2);
            }
            if (notificationSummary.TextCount > 0)
            {
                Debug.Print("Text: " + notificationSummary.TextCount.ToString());
                screen.DrawImage(58, 0, TextImage, 0, 0, TextImage.Width, TextImage.Height);
                screen.DrawText(notificationSummary.TextCount.ToString(), font, Color.White, 70, -2);
            }
            if (notificationSummary.VoiceCount > 0)
            {
                Debug.Print("Voice: " + notificationSummary.VoiceCount.ToString());
                screen.DrawImage(83, 0, VoiceMailImage, 0, 0, VoiceMailImage.Width, VoiceMailImage.Height);
                screen.DrawText(notificationSummary.VoiceCount.ToString(), font, Color.White, 95, -2);
            }
            if (notificationSummary.CalendarCount > 0)
            {
                Debug.Print("ToDo: " + notificationSummary.CalendarCount.ToString());
                screen.DrawImage(106, 0, CalendarImage, 0, 0, CalendarImage.Width, CalendarImage.Height);
                screen.DrawText(notificationSummary.CalendarCount.ToString(), font, Color.White, 117, -2);
            }


        }

    }
}