using System;
using Agent.Contrib;
using Agent.Contrib.Drawing;
using Agent.Contrib.Face;
using Agent.Contrib.Weather;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace WeatherFace
{
    public class WeatherWatchFace : IFace
    {
        public Agent.Contrib.Settings.ISettings Settings { get; set; }

        public IProvideWeather WeatherProvider { get; set; }

        private Drawing drawing = new Drawing();
        private Font font = Resources.GetFont(Resources.FontResources.Digital714point);
        private Font bigfont = Resources.GetFont(Resources.FontResources.Digital748TimeOnly);
        private Font smallFont = Resources.GetFont(Resources.FontResources.small);

        private int days = 3;
        private int buffer = 7;
        private int top = 10;

        public void Render(Microsoft.SPOT.Bitmap screen)
        {
            screen.DrawLine(Color.White, 2, 0, AGENT.Size / 2, AGENT.Size, AGENT.Size / 2);

            DateTime now = DateTime.Now;
            int counter = (int)now.DayOfWeek;
            counter++;
            int left = buffer;
            IForecast nowForecast = null;
            bool needsDate = false;
            DateTime lastUpdated = DateTime.Now;
            for (int x = 0; x <= days; x++)
            {
                if (counter >= 7) counter = 0;
                string dayName = System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames[counter];
                if (dayName.Length >= 3) dayName = dayName.Substring(0, 3);
                int width = drawing.MeasureString(dayName, font);
                screen.DrawText(dayName, font, Color.White, left, top);
                if (WeatherProvider != null && WeatherProvider.CurrentWeekForecast != null && WeatherProvider.CurrentWeekForecast.Count > 0)
                {
                    IForecast current = WeatherProvider.CurrentWeekForecast[0] as IForecast;
                    var startDate = now.Date.AddDays(x);
                    foreach (IForecast f in WeatherProvider.CurrentWeekForecast)
                    {
                        if (f.TimeStamp.Year == now.Date.Year && f.TimeStamp.Month == now.Date.Month && f.TimeStamp.Day == now.Date.Day)
                            nowForecast = f;
                        if (startDate.Year == f.TimeStamp.Year && startDate.Month == f.TimeStamp.Month && startDate.Day == f.TimeStamp.Day)
                        {
                            Debug.Print("Found match");
                            current = f;
                            break;
                        }
                    }
                    if (current != null)
                    {
                        needsDate = true;
                        screen.DrawText(current.Temperature.ToString(), font, Color.White, left + 4, top + font.Height + 2);
                        lastUpdated = current.TimeStamp;
                    }
                }
                counter++;
                if (counter > 6) counter = 0;
                left += width + buffer;
            }
            if (nowForecast != null)
            {
                string display = nowForecast.Temperature.ToString();
                int forecastLeft = AGENT.Size - drawing.MeasureString(display, bigfont);
                screen.DrawText(display, bigfont, Color.White, forecastLeft, (AGENT.Size / 2) + 2);
                needsDate = true;
                lastUpdated = nowForecast.TimeStamp;
            }
            if (needsDate)
            {
                screen.DrawText(lastUpdated.ToString(), smallFont, Color.White, 3, (AGENT.Size / 2) - smallFont.Height - 1);

            }

        }

        public int UpdateSpeed
        {
            get { return 60*1000; }
        }
    }
}
