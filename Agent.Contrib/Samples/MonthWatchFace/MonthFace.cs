using System;
using System.Collections;
using System.Globalization;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Agent.Contrib.Notifications;
using Agent.Contrib.Settings;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace MonthWatchFace
{
    public class MonthFace : FaceWithTrayBase, IFace
    {
        public MonthFace(IProvideNotifications _notifications, ISettings settings)
            : base(_notifications, settings)
        {

            DateTime startDate = Settings.Now.AddDays(-7);
            DateTime endDate = Settings.Now.AddDays(21);

            int col = (int) startDate.DayOfWeek;
            int row = 0;
            TimeSpan diff = endDate - startDate;
            int rowStart = AGENT.Size/2 + 7;
            int colStart = 6;
            int colPos = 0;
            int rowPos = 0;
            for (int i = 0; i < diff.Days; i++)
            {
                DateTime current = startDate.AddDays(i);
                int width = current.Day.ToString().Length;
                switch (col)
                {
                    case 0:
                        colPos = colStart - width;
                        break;
                    case 1:
                        colPos = colStart + 20-width;
                        break;
                    case 2:
                        colPos = colStart + 40 - width;
                        break;
                    case 3:
                        colPos = colStart + 57 - width;
                        break;
                    case 4:
                        colPos = colStart + 75 - width;
                        break;
                    case 5:
                        colPos = colStart + 91 - width;
                        break;
                    default:
                        colPos = colStart + 108 - width;
                        break;
                };

                switch (row)
                {
                    case 0:
                        rowPos = rowStart;
                        break;
                    case 1:
                        rowPos = rowStart + 14;
                        break;
                    case 2:
                        rowPos = rowStart + 28;
                        break;
                    case 3:
                        rowPos = rowStart + 42;
                        break;
                    default:
                        rowPos = rowStart + 66;
                        break;
                };
                days.Add(new Day()
                    {
                        Text = current.Day.ToString(),
                        Point = new Point() { X=colPos,Y=rowPos},
                        Timestamp = current
                    });

                col++;
                if (col >= 7)
                {
                    row++;
                    col = 0;
                }
            }
        }

        ArrayList days = new ArrayList();
        private Font font = Resources.GetFont(Resources.FontResources.small);
        private Bitmap monthGrid = new Bitmap(Resources.GetBytes(Resources.BinaryResources.MonthGrid), Bitmap.BitmapImageType.Gif);

        public override void Render(Bitmap screen)
        {
            if (base._screen == null) _screen = screen;
            screen.DrawLine(Color.White, 2, 0, AGENT.Size/2 - 10, AGENT.Size, AGENT.Size/2 - 10);
            screen.DrawImage(0, AGENT.Size - monthGrid.Height, monthGrid, 0, 0, monthGrid.Width, monthGrid.Height);
            foreach (Day day in days)
            {
                if (day.Timestamp.Date == Settings.Now.Date)
                {
                    screen.DrawRectangle(Color.White, 1, day.Point.X-2, day.Point.Y+1, 19, 11, 0, 0, Color.White, 0, 0,
                                         Color.White, 0, 0, 255);
                    screen.DrawText(day.Text, font, Color.Black, day.Point.X, day.Point.Y);
                }
                else
                {
                    screen.DrawText(day.Text, font, Color.White, day.Point.X, day.Point.Y);
                }

            }

            string dow = "Day: " + Settings.Now.DayOfYear.ToString();
            screen.DrawText(dow.ToString(), font, Color.White, 5, 35);

            string date = Settings.Now.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo.LongDatePattern);
            //string date = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames[(int) Settings.Now.Month];
            //date = date + " " + Settings.Now.Day.ToString() +;
            screen.DrawText(date, font, Color.White, 5, 15);

            

            drawing.DrawTray(screen, _notificationProvider, smallFont);

        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
