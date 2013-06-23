using System;
using System.Globalization;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Agent.Contrib.Notifications;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace AnalogWatchFace
{
    public class AnalogFace : IFace
    {
        private IProvideNotifications _notificationProvider; 
        public AnalogFace(IProvideNotifications notificationProvider)
        {
            _notificationProvider = notificationProvider;
            notificationProvider.OnNotificationReceived += notificationProvider_OnNotificationReceived;   
        }

        private void notificationProvider_OnNotificationReceived(INotification notification)
        {
            if (_screen != null)
            {
                _screen.Clear();
                Render(_screen);
                _screen.Flush();
            }
        }

        public Agent.Contrib.Settings.ISettings Settings { get; set; }
        private Font font = Resources.GetFont(Resources.FontResources.small);

        private Bitmap img = new Bitmap(Resources.GetBytes(Resources.BinaryResources.WatchFaceFromScratch),
                                Bitmap.BitmapImageType.Gif);

        private Bitmap envBmp = new Bitmap(Resources.GetBytes(Resources.BinaryResources.Envelope),
                                        Bitmap.BitmapImageType.Gif);

        private Bitmap mailBmp = new Bitmap(Resources.GetBytes(Resources.BinaryResources.Mail),
                                        Bitmap.BitmapImageType.Gif);

        private Bitmap timeBmp = new Bitmap(Resources.GetBytes(Resources.BinaryResources.Time),
                                        Bitmap.BitmapImageType.Gif);

        private Bitmap voiceBmp = new Bitmap(Resources.GetBytes(Resources.BinaryResources.VoiceMail),
                                        Bitmap.BitmapImageType.Gif);



        private Drawing drawing = new Drawing();
        private Bitmap _screen = null;
        public void Render(Bitmap screen)
        {
            if (_screen == null) _screen = screen;
            screen.DrawImage(0, 0, img, 0, 0, img.Width, img.Height);


            var text = "AGENT";
            Point textLocation = new Point(
                AGENT.Center.X - (drawing.MeasureString(text, font)/2), AGENT.Center.Y - 25);
            screen.DrawText(text, font, Color.White, textLocation.X, textLocation.Y);

            var date = Settings.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.MonthDayPattern);
            ; //time.MonthNameShort + " " + time.Day;

            Point dateLocation = new Point(
                AGENT.Center.X - (drawing.MeasureString(date, font)/2), AGENT.Center.Y + 20);
            screen.DrawText(date, font, Color.White, dateLocation.X, dateLocation.Y);


            drawing.PaintSkinnyHands(screen, Settings.Now, AGENT.Center);

            var notificationSummary = new NotificationSummary(_notificationProvider);
            if (notificationSummary.EmailCount > 0)
            {
                Debug.Print("Emails: " + notificationSummary.EmailCount.ToString());
                screen.DrawImage(20, AGENT.Center.Y - (mailBmp.Height / 2), mailBmp, 0, 0, mailBmp.Width, mailBmp.Height);
                screen.DrawText(notificationSummary.EmailCount.ToString(), font, Color.White, 21, (AGENT.Center.Y - (mailBmp.Height / 2)) + mailBmp.Height + 1);
            }
            if (notificationSummary.TextCount > 0)
            {
                Debug.Print("Text: " + notificationSummary.TextCount.ToString());
                screen.DrawImage(35, AGENT.Center.Y - (envBmp.Height / 2), envBmp, 0, 0, envBmp.Width, envBmp.Height);
                screen.DrawText(notificationSummary.TextCount.ToString(), font, Color.White, 36, (AGENT.Center.Y - (envBmp.Height / 2)) + envBmp.Height + 1);
            }
            if (notificationSummary.VoiceCount > 0)
            {
                Debug.Print("Voice: " + notificationSummary.VoiceCount.ToString());
                screen.DrawImage(80, AGENT.Center.Y - (voiceBmp.Height / 2), voiceBmp, 0, 0, voiceBmp.Width, voiceBmp.Height);
                screen.DrawText(notificationSummary.VoiceCount.ToString(), font, Color.White, 81, (AGENT.Center.Y - (voiceBmp.Height / 2)) + voiceBmp.Height + 1);
            }
            if (notificationSummary.CalendarCount > 0)
            {
                Debug.Print("ToDo: " + notificationSummary.CalendarCount.ToString());
                screen.DrawImage(95, AGENT.Center.Y - (timeBmp.Height / 2), timeBmp, 0, 0, timeBmp.Width, timeBmp.Height);
                screen.DrawText(notificationSummary.CalendarCount.ToString(), font, Color.White, 96, (AGENT.Center.Y - (timeBmp.Height / 2)) + timeBmp.Height + 1);
            }



        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
